using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private readonly ICollection<IEquipment> equipment;
        private readonly ICollection<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }

        public string Name 
        { 
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Gym name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => EquipmentWeightSum();

        public ICollection<IEquipment> Equipment => equipment;

        public ICollection<IAthlete> Athletes => athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count == Capacity)
            {
                throw new InvalidOperationException("Not enough space in the gym.");
            }
            athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete) => athletes.Remove(athlete);

        public void AddEquipment(IEquipment equipment) => this.equipment.Add(equipment);

        public void Exercise()
        {
            foreach (IAthlete athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} is a {GetType().Name}:");
            sb.Append("Athletes: ");

            if (athletes.Count > 0)
            {
                List<string> athletesNames = new List<string>();
                foreach (IAthlete athlete in athletes)
                {
                    athletesNames.Add(athlete.FullName);
                }
                sb.AppendLine(string.Join(", ", athletesNames));
            }
            else
            {
                sb.AppendLine("No athletes");
            }

            sb.AppendLine($"Equipment total count: {equipment.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");

            return sb.ToString().TrimEnd();
        }

        private double EquipmentWeightSum() => equipment.Sum(w => w.Weight);
    }
}
