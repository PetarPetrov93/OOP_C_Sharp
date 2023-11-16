using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private double pricePerNight;

        protected Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
        }

        public int BedCapacity { get; private set; }

        public double PricePerNight 
        { 
            get => pricePerNight;
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative!");
                }
                pricePerNight = value;
            } 
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
