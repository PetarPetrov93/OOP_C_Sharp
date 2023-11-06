using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Hen : Bird
    {
        private const double gainWeightRate = 0.35;
        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize, gainWeightRate) { }

        public override string ProduceSound() => "Cluck";

        public override void Eat(string food, int quantity)
        {
            if (food == "Vegetable" || food == "Meat" || food == "Seeds" || food == "Fruit")
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
