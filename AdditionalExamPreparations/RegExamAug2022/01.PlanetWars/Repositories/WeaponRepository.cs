using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly ICollection<IWeapon> weapons;
        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => weapons.ToList().AsReadOnly();

        public void AddItem(IWeapon model) => weapons.Add(model);

        public IWeapon FindByName(string name) => weapons.FirstOrDefault(w => w.GetType().Name == name);

        public bool RemoveItem(string name) => weapons.Remove(FindByName(name));
    }
}
