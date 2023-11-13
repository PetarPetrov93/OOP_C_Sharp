using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private readonly ICollection<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryCapacity;
            ConvertionCapacityIndex = convertionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public string Model 
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or empty.");
                }
                model = value;
            }
        }

        public int BatteryCapacity 
        { 
            get => batteryCapacity;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Battery capacity cannot drop below zero.");
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public int ConvertionCapacityIndex { get; private set; }

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.ToList().AsReadOnly();

        public void Eating(int minutes)
        {
            BatteryLevel += minutes * ConvertionCapacityIndex;
            if (BatteryLevel > BatteryCapacity)
            {
                BatteryLevel = BatteryCapacity;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
            interfaceStandards.Add(supplement.InterfaceStandard);
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.AppendLine($"--Supplements installed: {(interfaceStandards.Any() ? string.Join(" ", interfaceStandards) : "none")}"); // should work

            return sb.ToString().TrimEnd();
        }

    }
}
