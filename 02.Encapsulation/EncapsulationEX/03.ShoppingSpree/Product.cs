using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ShoppingSpree
{
    internal class Product
    {
        private string name;
        private int cost;

        public Product(string name, int  cost)
        {
            Name = name;
            Cost = cost;
        }

        //not sure if I need to validate the input, will have to check with Judge
        public string Name 
        { 
            get { return name; } 
            private set 
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                name = value; 
            } 
        }

        public int Cost 
        { 
            get { return cost; } 
            private set 
            {
                if (value < 0)
                {
                    throw new Exception("Money cannot be negative");
                }
                cost = value; 
            } 
        }

    }
}
