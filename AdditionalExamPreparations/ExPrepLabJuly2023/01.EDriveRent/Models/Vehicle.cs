using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
        }

        public string Brand
        { 
            get => brand; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand cannot be null or whitespace!");
                }
                brand = value;
            } 
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or whitespace!");
                }
                model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("License plate number is required!");
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; } = 100;

        public bool IsDamaged { get; private set; }

        public void Drive(double mileage)
        {
            BatteryLevel -= (int)Math.Round((mileage / MaxMileage) * 100);

            if (GetType() == typeof(CargoVan))
            {
                BatteryLevel -= 5;
            }
        }
        public void Recharge() => BatteryLevel = 100;

        public void ChangeStatus() => IsDamaged = IsDamaged ? false : true;

        public override string ToString() => $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {(IsDamaged ? "damaged" : "OK")}";
    }
}
