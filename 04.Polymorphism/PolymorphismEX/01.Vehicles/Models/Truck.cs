using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Truck : IDrivable
    {
        private const double additionalSummerFuelConsumption = 1.6;
        private const double fuelLeakage = 0.95;

        public Truck(double fuelQuantity, double consumptionPerKM)
        {
            FuelQuantity = fuelQuantity;
            ConsumptionPerKM = consumptionPerKM;
        }

        public double FuelQuantity {get; private set;}
        public double ConsumptionPerKM { get; private set;}

        public string Drive(double km)
        {
            if ((additionalSummerFuelConsumption + ConsumptionPerKM) * km is var total && total <= FuelQuantity)
            {
                FuelQuantity -= total;
                return $"{this.GetType().Name} travelled {km} km";
            }
            return $"{this.GetType().Name} needs refueling";
        }

        public void Refuel(double fuel)
        {
            FuelQuantity += fuel * fuelLeakage;
        }
        public override string ToString() => $"{this.GetType().Name}: {FuelQuantity:f2}";
    }
}
