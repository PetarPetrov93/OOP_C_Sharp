using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Models
{
    public class Rogue : BaseHero
    {
        private const int power = 80;
        public Rogue(string name) : base(name, power)
        {
        }

        public override string CastAbility() => $"{GetType().Name} - {Name} hit for {Power} damage";
    }
}
