using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string bakingTechnique;
        private int weighInGrams;

        public Dough(string flourType, string bakingTechnique, int weighInGrams)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            WeighInGrams = weighInGrams;
        }
        public string FlourType 
        { 
            get { return flourType; }
            private set 
            {
                if (!(value.ToLower() == "white" || value.ToLower() == "wholegrain"))
                {
                    throw new Exception("Invalid type of dough.");
                }
                flourType = value;
            }
        }
        public string BakingTechnique
        {
            get { return bakingTechnique; }
            private set
            {
                if (!(value.ToLower() == "crispy" || value.ToLower() == "chewy" || value.ToLower() == "homemade"))
                {
                    throw new Exception("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }
        public int WeighInGrams
        {
            get { return weighInGrams; }
            private set
            {
                if (value <= 0 || value > 200)
                {
                    throw new Exception("Dough weight should be in the range [1..200].");
                }
                weighInGrams = value;
            }
        }
        public double CaloriesPerGram { get { return CalculateCaloriesPerGram(); } }

        private double CalculateCaloriesPerGram()
        {
            double caloriesPerGram = 2;
            if (FlourType.ToLower() == "white")
            {
                caloriesPerGram *= 1.5;
            }
            else if (FlourType.ToLower() == "wholegrain")
            {
                caloriesPerGram *= 1;
            }
            if (BakingTechnique.ToLower() == "crispy")
            {
                caloriesPerGram *= 0.9;
            }
            else if (BakingTechnique.ToLower() == "chewy")
            {
                caloriesPerGram *= 1.1;
            }
            else if (BakingTechnique.ToLower() == "hommemade")
            {
                caloriesPerGram *= 1;
            }
            return caloriesPerGram;
        }

        
    }
}
