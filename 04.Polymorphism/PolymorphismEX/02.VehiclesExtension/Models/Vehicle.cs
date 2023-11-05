using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : IDrivable
    {
        private double increasedConsumption;
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double consumptionPerKM, double tankCapacity, double increasedConsumption)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            ConsumptionPerKM = consumptionPerKM;
            this.increasedConsumption = increasedConsumption;
        }

        public double FuelQuantity 
        {
            get { return fuelQuantity; }
            private set 
            {
                if (TankCapacity < value)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }

        public double ConsumptionPerKM { get; private set; }

        public double TankCapacity { get; private set; }



        public string Drive(double km, bool isIncreasedConsumption = true)
        {
            double consumption = isIncreasedConsumption ? ConsumptionPerKM + increasedConsumption : ConsumptionPerKM;

            if (FuelQuantity < km * consumption)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= km * consumption;
            return $"{this.GetType().Name} travelled {km} km";
        }


        public virtual void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (fuel + FuelQuantity > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
            }
            FuelQuantity += fuel;
        }


        public override string ToString() => $"{this.GetType().Name}: {FuelQuantity:f2}";
    }
}
