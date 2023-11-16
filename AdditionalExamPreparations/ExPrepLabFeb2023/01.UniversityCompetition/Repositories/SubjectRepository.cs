using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private readonly ICollection<ISubject> models;
        public SubjectRepository()
        {
            models = new List<ISubject>();
        }
        public IReadOnlyCollection<ISubject> Models => models.ToList().AsReadOnly();

        public void AddModel(ISubject model) => models.Add(model);

        public ISubject FindById(int id) => models.FirstOrDefault(m => m.Id == id);

        public ISubject FindByName(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
