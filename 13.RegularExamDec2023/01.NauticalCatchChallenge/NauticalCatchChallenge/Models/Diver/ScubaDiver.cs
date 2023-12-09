using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models.Diver
{
    public class ScubaDiver : Diver
    {
        private const int oxygenLevel = 540;
        public ScubaDiver(string name) : base(name, oxygenLevel)
        {
        }

        public override void Miss(int timeToCatch)
        {
            OxygenLevel -= (int)Math.Round(timeToCatch * 0.3, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy() => OxygenLevel = oxygenLevel;
    }
}
