using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private readonly ICollection<IBooking> books;

        public BookingRepository()
        {
           books = new List<IBooking>();
        }
        public void AddNew(IBooking model) => books.Add(model);

        public IReadOnlyCollection<IBooking> All() => books.ToList().AsReadOnly();

        public IBooking Select(string criteria) => books.FirstOrDefault(b => b.BookingNumber == int.Parse(criteria));
    }
}
