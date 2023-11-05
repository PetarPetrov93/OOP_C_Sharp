using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double increasedConsumption = 1.4;
        public Bus(double fuelQuantity, double consumptionPerKM, double tankCapacity) : base(fuelQuantity, consumptionPerKM, tankCapacity, increasedConsumption)
        {
        }

    }
}
