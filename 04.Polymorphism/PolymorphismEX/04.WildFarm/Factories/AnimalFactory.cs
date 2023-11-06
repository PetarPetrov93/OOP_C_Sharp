using _04.WildFarm.Factories.Interfaces;
using _04.WildFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        public Animal Create(string[] arguments)
        {
            string type = arguments[0];
            string name = arguments[1];
            double weight = double.Parse(arguments[2]);

            if (type == "Hen")
            {
                return new Hen(name, weight, double.Parse(arguments[3]));
            }
            else if (type == "Owl")
            {
                return new Owl(name, weight, double.Parse(arguments[3]));
            }
            else if (type == "Mouse")
            {
                return new Mouse(name, weight, arguments[3]);
            }
            else if (type == "Dog")
            {
                return new Dog(name, weight, arguments[3]);
            }
            else if (type == "Cat")
            {
                return new Cat(name, weight, arguments[3], arguments[4]);
            }
            else if (type == "Tiger")
            {
                return new Tiger(name, weight, arguments[3], arguments[4]);
            }
            else
            {
                throw new ArgumentException("Invalid animal!");
            }
        }
    }
}
