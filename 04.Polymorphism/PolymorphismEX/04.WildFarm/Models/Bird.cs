using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.WildFarm.Models
{
    public abstract class Bird : Animal
    {
        protected Bird(string name, double weight, double wingSize, double weightGainRate) : base(name, weight, weightGainRate)
        {
            WingSize = wingSize;
        }

        public double WingSize { get; private set; }

        public override string ToString() => $"{base.ToString()}{WingSize}, {Weight}, {FoodEaten}]";
    }
}
