using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ShoppingSpree
{
    internal class Person
    {
        private string name;
        private int money;
        private List<Product> bagOfProducts;

        public Person(string name, int money)
        {
            Name = name;
            Money = money;
            bagOfProducts = new List<Product>();
        }

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
        public int Money 
        { 
            get { return money; } 
            set 
            {
                if (value < 0)
                {
                    throw new Exception("Money cannot be negative");
                }
                money = value;
            } 
        }
        public void BuyProduct(Product product)
        {
            if (CanAffordToBuy(product))
            {
                bagOfProducts.Add(product);
                Console.WriteLine($"{Name} bought {product.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} can't afford {product.Name}");
            }
        }

        private bool CanAffordToBuy(Product product)
        {
            if (Money >= product.Cost)
            {
                Money -= product.Cost;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (bagOfProducts.Count > 0)
            {
                return $"{Name} - {string.Join(", ", GetProductsName(bagOfProducts))}";
            }
            return $"{Name} - Nothing bought";
        }

        private List<string> GetProductsName(List<Product> getTheNameOfTheProducts)
        {
            List<string> productsName = new List<string>();
            foreach (Product product in getTheNameOfTheProducts)
            {
                productsName.Add(product.Name);
            }
            return productsName;
        }

    }
}
