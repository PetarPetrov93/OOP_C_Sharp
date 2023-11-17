using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private double militaryPower;
        private readonly IRepository<IMilitaryUnit> army;
        private readonly IRepository<IWeapon> weapons;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            army = new UnitRepository();
            weapons = new WeaponRepository();
        }

        public string Name 
        { 
            get => name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value)) // it says null or whitespace however the message says null or empty
                {
                    throw new ArgumentException("Planet name cannot be null or empty.");
                }
                name = value;
            } 
        }

        public double Budget 
        {
            get => budget;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget's amount cannot be negative.");
                }
                budget = value;
            } 
        }

        public double MilitaryPower => CalculateMilitaryPower(); // I Think it should be a getter, even tho in the task they say to keep the setter private and to use value, might need to change it

        public IReadOnlyCollection<IMilitaryUnit> Army => army.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit) => army.AddItem(unit);

        public void AddWeapon(IWeapon weapon) => weapons.AddItem(weapon);

        public void TrainArmy()
        {
            foreach (IMilitaryUnit unit in Army)
            {
                unit.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException("Budget too low!");
            }

            Budget -= amount;
        }

        public void Profit(double amount) => Budget += amount;

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.Append("--Forces: ");

            if (Army.Count == 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                List<string> unitNames = new List<string>();
                foreach (IMilitaryUnit unit in Army)
                {
                    unitNames.Add(unit.GetType().Name);
                }
                sb.AppendLine($"{string.Join(", ", unitNames)}");
            }

            sb.Append("--Combat equipment: ");

            if (Weapons.Count == 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                List<string> weaponsNames = new List<string>();
                foreach (IWeapon weapon in Weapons)
                {
                    weaponsNames.Add(weapon.GetType().Name);
                }
                sb.AppendLine($"{string.Join(", ", weaponsNames)}");
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        private double CalculateMilitaryPower()
        {
            militaryPower = Army.Sum(a => a.EnduranceLevel) + Weapons.Sum(w => w.DestructionLevel);

            if (Army.Any(a => a.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                militaryPower += militaryPower * 0.3;
            }
            if (Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                militaryPower += militaryPower * 0.45;
            }

            return Math.Round(militaryPower, 3);
        }
    }
}
