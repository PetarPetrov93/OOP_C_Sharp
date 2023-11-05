using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double increasedConsumption = 1.6;
        private const double fuelLeakage = 0.95;

        public Truck(double fuelQuantity, double consumptionPerKM, double tankCapacity) : base(fuelQuantity, consumptionPerKM, tankCapacity, increasedConsumption)
        {}

        public override void Refuel(double fuel)
        {
            if (fuel + FuelQuantity > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
            }

            base.Refuel(fuel * fuelLeakage);
        }
    }
}
