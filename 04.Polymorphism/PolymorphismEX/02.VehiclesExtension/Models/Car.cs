using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double increasedConsumption = 0.9;

        public Car(double fuelQuantity, double consumptionPerKM, double tankCapacity) : base(fuelQuantity, consumptionPerKM, tankCapacity, increasedConsumption)
        {}
    }
}
