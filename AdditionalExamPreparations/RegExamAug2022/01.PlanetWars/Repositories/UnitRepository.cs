using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly ICollection<IMilitaryUnit> militaryUnits;
        public UnitRepository()
        {
            militaryUnits = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => militaryUnits.ToList().AsReadOnly();

        public void AddItem(IMilitaryUnit model) => militaryUnits.Add(model);

        public IMilitaryUnit FindByName(string name) => militaryUnits.FirstOrDefault(u => u.GetType().Name == name);

        public bool RemoveItem(string name) => militaryUnits.Remove(FindByName(name));
    }
}
