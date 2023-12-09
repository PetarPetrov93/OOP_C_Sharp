using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models.Diver
{
    public class FreeDiver : Diver
    {
        private const int oxygenLevel = 120; 
        public FreeDiver(string name) : base(name, oxygenLevel) { }

        public override void Miss(int timeToCatch)
        {
            OxygenLevel -= (int)Math.Round(timeToCatch * 0.6, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy() => OxygenLevel = oxygenLevel;
    }
}
