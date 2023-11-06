using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Mouse : Mammal
    {
        private const double gainWeightRate = 0.10;
        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion, gainWeightRate) { }

        public override string ProduceSound() => "Squeak";


        public override void Eat(string food, int quantity)
        {
            if (food == "Vegetable" || food == "Fruit")
            {
                Weight += quantity * gainWeightRate;
                FoodEaten += quantity;
            }
            else
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food}!");
            }
        }
        public override string ToString() => $"{base.ToString()}{Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
