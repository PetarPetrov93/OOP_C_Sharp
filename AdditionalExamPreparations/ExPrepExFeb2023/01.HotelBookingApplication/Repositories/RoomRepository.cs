using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private readonly ICollection<IRoom> rooms;
        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }
        public void AddNew(IRoom model) => rooms.Add(model);

        public IReadOnlyCollection<IRoom> All() => rooms.ToList().AsReadOnly();

        public IRoom Select(string criteria) => rooms.FirstOrDefault(r => r.GetType().Name == criteria); // I think this is what they mean in the task
    }
}
