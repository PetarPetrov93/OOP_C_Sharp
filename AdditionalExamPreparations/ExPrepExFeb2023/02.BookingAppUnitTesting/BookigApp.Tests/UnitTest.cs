using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RoomConstructorShouldWorkProperly()
        {
            Room room = new Room(2, 25.5);

            Assert.AreEqual(2, room.BedCapacity);
            Assert.AreEqual(25.5, room.PricePerNight);
        }

        [Test]
        public void RoomBedCapacityShouldThrowExceptionWhenGiven0()
        {
            Room room = null;
            Assert.Throws<ArgumentException>(() => room = new Room(0, 25.5));
        }

        [Test]
        public void RoomBedCapacityShouldThrowExceptionWhenGivenNegative()
        {
            Room room = null;
            Assert.Throws<ArgumentException>(() => room = new Room(-5, 25.5));
        }

        [Test]
        public void PricePerNightCapacityShouldThrowExceptionWhenGiven0()
        {
            Room room = null;
            Assert.Throws<ArgumentException>(() => room = new Room(2, 0));
        }

        [Test]
        public void PricePerNightCapacityShouldThrowExceptionWhenGivenNegative()
        {
            Room room = null;
            Assert.Throws<ArgumentException>(() => room = new Room(2, -10));
        }

        [Test]
        public void BookingConstructorShouldWork()
        {
            Room room = new Room(2, 25.5);

            Booking booking = new Booking(1, room, 2);

            Assert.IsNotNull(room);
            Assert.AreEqual(2, booking.Room.BedCapacity);
            Assert.AreEqual(25.5, booking.Room.PricePerNight);
            Assert.AreEqual(1, booking.BookingNumber);
            Assert.AreEqual(2, booking.ResidenceDuration);
        }

        [Test]
        public void HotelConstructorShouldWorkProperly()
        {
            Hotel hotel = new Hotel("Eleon", 5);

            Assert.IsNotNull(hotel.Bookings);
            Assert.IsNotNull(hotel.Rooms);
            Assert.AreEqual("Eleon", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Rooms.Count);
        }

        [Test]
        public void HotelFullNamePropertyShouldThorwExceptionWhenNullGiven()
        {
            Hotel hotel = null;

            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(null, 5));
        }

        [Test]
        public void HotelFullNamePropertyShouldThorwExceptionWhenQhiteSpaceGiven()
        {
            Hotel hotel = null;

            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(" ", 5));
        }

        [Test]
        public void CategoryPropertyShouldThrowExceptionWhenLessThan1Given()
        {
            Hotel hotel = null;

            Assert.Throws<ArgumentException>(() => hotel = new Hotel("Eleon", 0));
            Assert.Throws<ArgumentException>(() => hotel = new Hotel("Eleon", -2));
        }

        [Test]
        public void CategoryPropertyShouldThrowExceptionWhenMoreThan5Given()
        {
            Hotel hotel = null;

            Assert.Throws<ArgumentException>(() => hotel = new Hotel("Eleon", 6));
        }

        [Test]
        public void HotelAddRoomMethodShouldAddRoom()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);

            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        public void HotelBookRoomShouldWorkProperly()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, 2, 500);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(51, hotel.Turnover);
        }

        [Test]
        public void HotelBookRoomShouldWorkProperlyButNotGetTurnoverWhenNoRoomIsAvailable()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, 2, 20);

            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void HotelBookRoomShouldThrowExceptionWhenAdultsAre0()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 1, 2, 500));
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void HotelBookRoomShouldThrowExceptionWhenAdultsAreNegativeNum()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(-5, 1, 2, 500));
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void HotelBookRoomShouldThrowExceptionWhenChildrenAreNegativeNum()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, -1, 2, 500));
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void HotelBookRoomShouldThrowExceptionWhenDurationIsLessThan1()
        {
            Room room = new Room(2, 25.5);
            Hotel hotel = new Hotel("Eleon", 5);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, 1, 0, 500));
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }
    }
}