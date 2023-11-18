using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly ICollection<IFormulaOneCar> models;

        public FormulaOneCarRepository()
        {
            models = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models => models.ToList().AsReadOnly();

        public void Add(IFormulaOneCar model) => models.Add(model);

        public IFormulaOneCar FindByName(string name) => models.FirstOrDefault(c => c.Model == name);

        public bool Remove(IFormulaOneCar model) => models.Remove(model);
    }
}
