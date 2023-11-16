using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            BookingNumber = bookingNumber;
        }

        public IRoom Room { get; private set; }

        public int ResidenceDuration 
        { 
            get => residenceDuration;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Duration cannot be negative or zero!");
                }
                residenceDuration = value;
            } 
        }

        public int AdultsCount
        {
            get => adultsCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Adults cannot be negative or zero!");
                }
                adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get => childrenCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Children cannot be negative or zero!");
                }
                childrenCount = value;
            }
        }

        public int BookingNumber { get; private set; }

        public string BookingSummary()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booking number: {BookingNumber}");
            sb.AppendLine($"Room type: {Room.GetType().Name}");
            sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            sb.AppendLine($"Total amount paid: {TotalPaid():F2} $");

            return sb.ToString().TrimEnd();
        }

        private double TotalPaid() => Math.Round(ResidenceDuration * Room.PricePerNight, 2);
    }
}
