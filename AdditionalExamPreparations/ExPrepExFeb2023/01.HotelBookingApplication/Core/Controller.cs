using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHotel> hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.Select(hotelName) is not null) //this should work
            {
                return $"Hotel {hotelName} is already registered in our platform.";
            }

            Hotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return $"{category} stars hotel {hotelName} is registered in our platform and expects room availability to be uploaded.";
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (hotels.Select(hotelName) is null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            IHotel hotel = hotels.Select(hotelName);

            if (hotel.Rooms.All().Any(r => r.GetType().Name == roomTypeName))
            {
                return "Room type is already created!";
            }

            if (roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment))
            {
                throw new ArgumentException("Incorrect room type!");
            }

            IRoom room = null;

            if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == nameof(Studio))
            {
                room = new Studio();
            }
            else if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }

            hotel.Rooms.AddNew(room);

            return $"Successfully added {roomTypeName} room type in {hotelName} hotel!";
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return $"Profile {hotelName} doesn’t exist!";
            }
            if (roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment))
            {
                throw new ArgumentException("Incorrect room type!");
            }

            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            if (!hotel.Rooms.All().Any(r => r.GetType().Name == roomTypeName))
            {
                return "Room type is not created yet!";
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);

            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException("Price is already set!");
            }
            room.SetPrice(price);

            return $"Price of {roomTypeName} room type in {hotelName} hotel is set!";
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return $"{category} star hotel is not available in our platform.";
            }

            foreach (IHotel hotel in hotels.All().Where(h => h.Category == category).OrderBy(h => h.FullName)) // they order it by turnover first, I have no idea why as it is not mentioned in the task
            {
                IRoom room = hotel.Rooms.All().Where(r => r.PricePerNight > 0 && r.BedCapacity >= adults + children).OrderBy(r => r.BedCapacity).FirstOrDefault();

                if (room != null)
                {
                    int bookingNumber = hotel.Bookings.All().Count() + 1; //int bookingNumber = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1; -> again, NO IDEA WHY!!!!!!

                    IBooking booking = new Booking(room, duration, adults, children, bookingNumber);

                    hotel.Bookings.AddNew(booking);

                    return $"Booking number {bookingNumber} for {hotel.FullName} hotel is successful!";
                }
            }

            return "We cannot offer appropriate room for your request.";
        }

        public string HotelReport(string hotelName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return $"Profile {hotelName} doesn’t exist!";
            }

            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            sb.Append(Environment.NewLine);

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (IBooking booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString().TrimEnd();
        }

    }
}
