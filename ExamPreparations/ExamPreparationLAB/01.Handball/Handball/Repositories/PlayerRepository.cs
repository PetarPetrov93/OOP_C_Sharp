using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> models;
        public PlayerRepository()
        {
            models = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => models.AsReadOnly();

        public void AddModel(IPlayer model) => models.Add(model);

        public bool ExistsModel(string name) =>models.Any(x => x.Name == name);

        public IPlayer GetModel(string name) => models.FirstOrDefault(x => x.Name == name);

        public bool RemoveModel(string name) => models.Remove(models.FirstOrDefault(x => x.Name == name));
    }
}
