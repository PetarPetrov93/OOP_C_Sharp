using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly ICollection<IUser> users;
        public UserRepository()
        {
            users = new List<IUser>();
        }
        public void AddModel(IUser model) => users.Add(model);

        public bool RemoveById(string identifier) => users.Remove(users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier));

        public IUser FindById(string identifier) => users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll() => users.ToList().AsReadOnly();

    }
}
