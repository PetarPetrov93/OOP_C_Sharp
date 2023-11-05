using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models.Interfaces
{
    public interface IDrivable
    {
        public double FuelQuantity { get; }
        public double ConsumptionPerKM { get; }
        string Drive(double km, bool isIncreasedConsumption = true);

        void Refuel(double fuel);
    }
}
