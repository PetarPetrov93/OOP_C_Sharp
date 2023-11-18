﻿using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models.FromulaCars
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsepower;
        private double engineDisplacement;

        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
        }

        public string Model 
        {
            get => model;
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException($"Invalid car model: {value}.");
                }
                model = value;
            } 
        }

        public int Horsepower 
        { 
            get => horsepower;
            private set 
            {
                if (value < 900 || value > 1050)
                {
                    throw new ArgumentException($"Invalid car horsepower: {value}.");
                }
                horsepower = value;
            } 
        }

        public double EngineDisplacement
        {
            get => engineDisplacement;
            private set
            {
                if (value < 1.6 || value > 2)
                {
                    throw new ArgumentException($"Invalid car engine displacement: {value}.");
                }
                engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps) => engineDisplacement / horsepower * laps;
    }
}
