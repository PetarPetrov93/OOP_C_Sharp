using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy
    {
        private const double stolenPrice = 3.5;
        public Stolen(string name) : base(name, stolenPrice) { }
    }
}
