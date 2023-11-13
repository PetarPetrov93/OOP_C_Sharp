using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly ICollection<IRobot> robots;
        public RobotRepository()
        {
            robots = new List<IRobot>();
        }
        public void AddNew(IRobot model) => robots.Add(model);

        public IRobot FindByStandard(int interfaceStandard) => robots.FirstOrDefault(r => r.InterfaceStandards.Contains(interfaceStandard));

        public IReadOnlyCollection<IRobot> Models() => robots.ToList().AsReadOnly();

        public bool RemoveByName(string typeName) => robots.Remove(robots.FirstOrDefault(r => r.GetType().Name == typeName));
    }
}
