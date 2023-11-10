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
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> models;
        public TeamRepository()
        {
            models = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => models.AsReadOnly();

        public void AddModel(ITeam model) => models.Add(model);

        public bool ExistsModel(string name) => models.Any(x => x.Name == name);

        public ITeam GetModel(string name) => models.FirstOrDefault(x => x.Name == name);

        public bool RemoveModel(string name) => models.Remove(models.FirstOrDefault(x => x.Name == name));
    }
}
