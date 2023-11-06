using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Models
{
    public class Paladin : BaseHero
    {
        private const int power = 100;
        public Paladin(string name) : base(name, power)
        {
        }

        public override string CastAbility() => $"{GetType().Name} - {Name} healed for {Power}";
    }
}
