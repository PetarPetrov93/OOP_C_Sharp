using _04.BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Models
{
    public class Person : IIdentifiable, IHasABirthday, IHuman, IBuyer
    {
        
        public Person(string name, int age, string iD, string birthday)
        {
            Name = name;
            Age = age;
            ID = iD;
            Birthday = birthday;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
        public string Birthday { get ; set ; }
        public int Food { get ; private set ; }

        public void BuyFood() => Food += 10;
    }
}
