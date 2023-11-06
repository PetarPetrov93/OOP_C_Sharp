using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public class Dog : Mammal
    {
        private const double gainWeightRate = 0.40;
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion, gainWeightRate) { }

        public override string ProduceSound() => "Woof!";

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
        public override string ToString() => $"{base.ToString()}{Weight}, {LivingRegion}, {FoodEaten}]";
    }

    
}
