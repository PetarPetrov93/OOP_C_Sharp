using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double weight = 10_000;
        private const decimal price = 80;
        public Kettlebell() : base(weight, price) { }
    }
}
