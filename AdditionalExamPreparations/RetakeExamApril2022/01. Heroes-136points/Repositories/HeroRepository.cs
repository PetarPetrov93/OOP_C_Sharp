
using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly ICollection<IHero> models;
        public HeroRepository()
        {
            models = new List<IHero>();
        }
        public IReadOnlyCollection<IHero> Models => models.ToList().AsReadOnly();

        public void Add(IHero model) => models.Add(model);

        public IHero FindByName(string name) => models.FirstOrDefault(h => h.Name == name);

        public bool Remove(IHero model) => models.Remove(model);
    }
}
