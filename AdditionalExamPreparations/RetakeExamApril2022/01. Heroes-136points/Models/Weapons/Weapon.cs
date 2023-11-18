using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;
        private int damage;

        protected Weapon(string name, int durability, int damage)
        {
            Name = name;
            Durability = durability;
            this.damage = damage;
        }

        public string Name 
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Weapon type cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Durability 
        { 
            get => durability;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Durability cannot be below 0.");
                }
                durability = value;
            } 
        }

        public int DoDamage()
        {
            
            if (Durability - 1 < 0)
            {
                Durability = 0;
                return 0;
            }

            Durability--;
            return damage;
        }
    }
}
