using _03.Raiding.Factories.Interfaces;
using _03.Raiding.Models;
using _03.Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Factories
{
    public class CreateAHero : ICreateAHero
    {
        public IHero CreateHero( string name, string type)
        {
            if (type == "Druid")
            {
                return new Druid(name);
            }
            else if (type == "Paladin")
            {
                return new Paladin(name);
            }
            else if (type == "Rogue")
            {
                return new Rogue(name);
            }
            else if (type == "Warrior")
            {
                return new Warrior(name);
            }
            else
            {
                throw new ArgumentException("Invalid hero!");
            }
        }
    }
}
