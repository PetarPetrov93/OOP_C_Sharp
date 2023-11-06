using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double gainWeightRate = 1;
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed, gainWeightRate) { }

        public override string ProduceSound() => "ROAR!!!";

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
