using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models.Fish
{
    public class PredatoryFish : Fish
    {
        private const int timeToCatch = 60;
        public PredatoryFish(string name, double points) : base(name, points, timeToCatch) { }
    }
}
