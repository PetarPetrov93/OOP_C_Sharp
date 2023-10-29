using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;
        private Dough dough;

        public Pizza(string name)
        {
            toppings = new List<Topping>();
            Name = name;
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length > 15)
                {
                    throw new Exception("Pizza name should be between 1 and 15 symbols.");
                }
                name = value;
            }
        }

        public Dough Dough 
        {
            get { return dough; }
            set { dough = value; }
        }

        public int NumberOfToppings { get { return toppings.Count; } } // Getter

        public string TotalCalories { get { return $"{CalculateTotalCalories() :f2}"; } } // Getter, formatted as string :f2

        private double CalculateTotalCalories()
        {
            double totalCalories = 0;

            totalCalories += dough.CaloriesPerGram * dough.WeighInGrams;

            foreach (var topping in toppings)
            {
                totalCalories += topping.CaloriesPerGram * topping.WeighInGrams;
            }

            return totalCalories;
        }

        public void AddTopping(Topping topping)
        {
            if (ToppingsCount())
            {
                toppings.Add(topping);
            }
            else 
            { 
                throw new Exception("Number of toppings should be in range [0..10]."); 
            }
        }

        private bool ToppingsCount()
        {
            if (toppings.Count < 10)
            {
                return true;
            }
            return false;
        }
    }
}
