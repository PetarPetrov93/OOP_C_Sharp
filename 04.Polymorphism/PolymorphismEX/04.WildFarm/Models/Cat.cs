using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Cat : Feline
    {
        private const double gainWeightRate = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed, gainWeightRate) { }

        public override void Eat(string food, int quantity)
        {
            if (food == "Vegetable" || food == "Meat")
            {
                Weight += quantity * gainWeightRate;
                FoodEaten += quantity;
            }
            else
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
        }

        public override string ProduceSound() => "Meow";
    }
}
