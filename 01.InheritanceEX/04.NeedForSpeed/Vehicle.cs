using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class Vehicle
    {
        public const double DefaultFuelConsumption = 1.25; //default value for fuel consumption
        public Vehicle(int horsePower, double fuel)
        {
            HorsePower = horsePower;
            Fuel = fuel;
        }

        // if done that way: { get; set; } = DefaultFuelConsumption; (or that way: { get; set; } = 1.25;) we will be able to set different FuelConsumption value through the constructor
        public virtual double FuelConsumption => DefaultFuelConsumption; //done with => means it is a read only and we cannot change this value through the constructor (can also be node => 1.25;) 

        public double Fuel { get; set; }
        public int HorsePower { get; set; }
        public virtual void Drive(double kilometers)
        {
            if (Fuel - (kilometers * FuelConsumption) >=0)
            {
                Fuel -= kilometers * FuelConsumption;
            } 
        }
    }
}
