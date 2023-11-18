using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        private readonly ICollection<IHero> knights;
        private readonly ICollection<IHero> barbarians;
        public Map()
        {
            knights = new List<IHero>();
            barbarians = new List<IHero>();
        }
        public string Fight(ICollection<IHero> players)
        {
            foreach (IHero hero in players)
            {
                if (hero.GetType().Name == nameof(Knight))
                {
                    knights.Add(hero);
                }
                else
                {
                    barbarians.Add(hero);
                }
            }
            //Alternative option if you don't create the two collections as fields.
            //ICollection<IHero> knights = players.Where(p => p.GetType().Name == nameof(Knight)).ToList();
            //ICollection<IHero> barbarians = players.Where(p => p.GetType().Name == nameof(Barbarian)).ToList();

            while (knights.Any(b => b.IsAlive == true) && barbarians.Any(b => b.IsAlive == true))
            {
                foreach (IHero knight in knights)
                {
                    if (knight.IsAlive)
                    {

                        foreach (IHero barbarian in barbarians)
                        {
                            if (barbarian.IsAlive)
                            {
                                barbarian.TakeDamage(knight.Weapon.DoDamage());
                            }

                        }
                    }
                }

                foreach (IHero barbarian in barbarians)
                {
                    if (barbarian.IsAlive)
                    {

                        foreach (IHero knight in knights)
                        {
                            if (knight.IsAlive)
                            {
                                knight.TakeDamage(barbarian.Weapon.DoDamage());
                            }

                        }
                    }
                }

            }

            int numberofDead = 0;

            if (knights.Any(b => b.IsAlive == true))
            {
                numberofDead = knights.Where(b => b.IsAlive == false).Count();
                return $"The knights took {numberofDead} casualties but won the battle.";
            }
            else
            {
                numberofDead = barbarians.Where(b => b.IsAlive == false).Count();
                return $"The barbarians took {numberofDead} casualties but won the battle.";
            }
        }
    }
}
