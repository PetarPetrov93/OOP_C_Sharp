using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public abstract class Feline : Mammal
    {
        protected Feline(string name, double weight, string livingRegion, string breed, double gainWeightRate) : base(name, weight, livingRegion, gainWeightRate)
        {
            Breed = breed;
        }
        public string Breed { get; private set; }

        public override string ToString() => $"{base.ToString()}{Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
