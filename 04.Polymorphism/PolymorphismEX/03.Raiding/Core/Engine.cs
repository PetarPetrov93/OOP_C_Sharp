using _03.Raiding.Core.Interfaces;
using _03.Raiding.Factories;
using _03.Raiding.Factories.Interfaces;
using _03.Raiding.IO.Interfaces;
using _03.Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICreateAHero createAHero; //in order to be able to use the CreateAHero class we need to have the abstraction - ICreateAHero as a field

        private readonly ICollection<IHero> heroes; // this is where we will store the created heroes

        public Engine(IReader reader, IWriter writer, ICreateAHero createAHero) // since ICreateAHero is an asbtraction we need to initialize what it will represent through the constructor and the main method
        {
            this.reader = reader;
            this.writer = writer;
            this.createAHero = createAHero;

            heroes = new List<IHero>(); // initializing the ICollection
        }
        public void Run()
        {
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string heroName = reader.ReadLine();
                string heroType = reader.ReadLine();
                try
                {
                    heroes.Add(createAHero.CreateHero(heroName, heroType));
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                    i--;
                }
            }

            int bossPower = int.Parse(reader.ReadLine());

            if (Battle(bossPower))
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }

        }

        private bool Battle(int bossPower)
        {
            int totalPower = 0;
            foreach (var hero in heroes)
            {
                totalPower += hero.Power;
                writer.WriteLine(hero.CastAbility());
            }
            if (totalPower >= bossPower)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
