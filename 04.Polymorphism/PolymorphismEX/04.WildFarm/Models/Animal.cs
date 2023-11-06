using _04.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public abstract class Animal : IProduceSound
    {
        private readonly double weightGainRate = 0;
        protected Animal(string name, double weight, double weightGainRate)
        {
            Name = name;
            Weight = weight;
            this.weightGainRate = weightGainRate;
        }

        public string Name { get; protected set; }
        public double Weight { get; protected set; }
        public int FoodEaten { get; protected set; }

        public abstract void Eat(string food, int quantity);
        public abstract string ProduceSound();

        public override string ToString() => $"{GetType().Name} [{Name}, ";

    }
}
