using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Owl : Bird
    {
        private const double gainWeightRate = 0.25;
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize, gainWeightRate) { }

        public override string ProduceSound() => "Hoot Hoot";

        public override void Eat(string food, int quantity)
        {
            if (food == "Meat")
            {
                Weight += quantity * gainWeightRate;
                FoodEaten += quantity;
            }
            else
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
        }
    }
}
