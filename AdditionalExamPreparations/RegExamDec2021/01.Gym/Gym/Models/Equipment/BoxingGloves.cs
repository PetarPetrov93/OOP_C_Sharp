using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const double weight = 227;
        private const decimal price = 120;
        public BoxingGloves() : base(weight, price)
        {
        }
    }
}
