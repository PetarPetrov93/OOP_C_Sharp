using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Models.Interfaces
{
    public interface IBuyer : IHuman
    {
        int Food { get; }
        public void BuyFood();
    }
}
