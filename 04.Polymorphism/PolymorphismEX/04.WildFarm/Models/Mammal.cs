using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public abstract class Mammal : Animal
    {
        protected Mammal(string name, double weight, string livingRegion, double gainWeightRate) : base(name, weight, gainWeightRate)
        {
            LivingRegion = livingRegion;
        }
        public string LivingRegion { get; private set; }

    }
}
