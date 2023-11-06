using _03.Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Factories.Interfaces
{
    public interface ICreateAHero
    {
        IHero CreateHero(string name, string type);
    }
}
