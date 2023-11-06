using _04.WildFarm.Core.Interfaces;
using _04.WildFarm.Factories;
using _04.WildFarm.Factories.Interfaces;
using _04.WildFarm.IO.Interfaces;
using _04.WildFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IAnimalFactory animalFactory;

        private readonly ICollection<Animal> animals;
        public Engine(IReader reader, IWriter writer, IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.animalFactory = animalFactory;
            animals = new List<Animal>();
        }
        public void Run()
        {
            string cmd;
            while ((cmd = reader.ReadLine()) != "End")
            {
                try
                {
                    string[] parameters = cmd.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Animal animal = animalFactory.Create(parameters);
                    writer.WriteLine(animal.ProduceSound());
                    animals.Add(animal);

                    string[] food = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    animal.Eat(food[0], int.Parse(food[1]));

                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
            foreach (var animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
