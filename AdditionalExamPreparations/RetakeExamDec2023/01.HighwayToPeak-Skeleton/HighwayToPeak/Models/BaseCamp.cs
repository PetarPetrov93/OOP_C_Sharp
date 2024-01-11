using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class BaseCamp : IBaseCamp
    {
        private readonly ICollection<string> residents;
        public BaseCamp()
        {
            residents = new List<string>();
        }

        public IReadOnlyCollection<string> Residents => residents.OrderBy(x => x).ToList().AsReadOnly();

        public void ArriveAtCamp(string climberName) => residents.Add(climberName);

        public void LeaveCamp(string climberName) => residents.Remove(residents.FirstOrDefault(name => name == climberName));
    }
}
