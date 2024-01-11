using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository : IRepository<IPeak>
    {
        private readonly ICollection<IPeak> peaks;
        public PeakRepository()
        {
            peaks = new List<IPeak>();
        }
        public IReadOnlyCollection<IPeak> All => peaks.ToList().AsReadOnly();

        public void Add(IPeak model) => peaks.Add(model);

        public IPeak Get(string name) => peaks.FirstOrDefault(p => p.Name == name);
    }
}
