using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models.Pilot
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            FullName = fullName;
        }
        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid pilot name: {value}.");
                }
                fullName = value;
            }
        }

        public IFormulaOneCar Car 
        {
            get => car;
            private set 
            {
                if (value is null)
                {
                    throw new NullReferenceException("Pilot car can not be null.");
                }
                car = value;
            }
        }

        public int NumberOfWins { get; private set; }

        public bool CanRace { get; private set; }

        public void AddCar(IFormulaOneCar car)
        {
            Car = car;
            CanRace = true;
        }
        public void WinRace() => NumberOfWins++;

        public override string ToString() => $"Pilot {FullName} has {NumberOfWins} wins.";

    }
}
