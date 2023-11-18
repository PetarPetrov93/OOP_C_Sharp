using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly ICollection<IWeapon> models;
        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => models.ToList().AsReadOnly();

        public void Add(IWeapon model) => models.Add(model);

        public IWeapon FindByName(string name) => models.FirstOrDefault(w => w.Name == name);

        public bool Remove(IWeapon model) => models.Remove(model);
    }
}
