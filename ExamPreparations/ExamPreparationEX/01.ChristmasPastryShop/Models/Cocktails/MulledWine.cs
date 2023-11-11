using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine : Cocktail
    {
        private const double largeMulledWinePrice = 13.5;
        public MulledWine(string name, string size) : base(name, size, largeMulledWinePrice) { }
    }
}
