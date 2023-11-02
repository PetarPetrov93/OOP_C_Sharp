using _04.BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Models
{
    public class Rebel : IHuman, IBuyer
    {
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }

        public string Name { get ; set ; }
        public int Age { get ; set ; }
        public string Group { get ; set ;}
        public int Food { get; private set; }

        public void BuyFood() => Food += 5;
    }
}
