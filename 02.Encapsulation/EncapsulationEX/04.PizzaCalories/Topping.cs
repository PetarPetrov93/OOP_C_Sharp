using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Topping
    {
        private string toppingName;
        private int weighInGrams;

        public Topping(string toppingName, int weighInGrams)
        {
            ToppingName = toppingName;
            WeighInGrams = weighInGrams;
        }

        public string ToppingName
        {
            get { return toppingName; }
            private set 
            {
                if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new Exception($"Cannot place {value} on top of your pizza.");
                }
                toppingName = value; 
            }
        }
        public int WeighInGrams
        {
            get { return weighInGrams;}
            private set
            {
                if (value < 0 || value > 50)
                {
                    throw new Exception($"{ToppingName} weight should be in the range [1..50].");
                }
                weighInGrams = value;
            }
        }

        public double CaloriesPerGram { get { return CalculateCaloriesPerGram(); } }

        private double CalculateCaloriesPerGram()
        {
            double caloriesPerGram = 2;
            if (ToppingName.ToLower() == "meat")
            {
                caloriesPerGram *= 1.2;
            }
            else if (ToppingName.ToLower() == "veggies")
            {
                caloriesPerGram *= 0.8;
            }
            else if (ToppingName.ToLower() == "cheese")
            {
                caloriesPerGram *= 1.1;
            }
            else if (ToppingName.ToLower() == "sauce")
            {
                caloriesPerGram *= 0.9;
            }
            return caloriesPerGram;
        }

    }
}
