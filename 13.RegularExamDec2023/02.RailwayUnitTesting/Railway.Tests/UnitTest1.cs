namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using NUnit.Framework.Constraints;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldWork()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            Assert.AreEqual("Gara Levski", railwayStation.Name);
            Assert.IsNotNull(railwayStation.DepartureTrains);
            Assert.IsNotNull(railwayStation.ArrivalTrains);
            Assert.AreEqual(0, railwayStation.DepartureTrains.Count);
            Assert.AreEqual(0, railwayStation.ArrivalTrains.Count);
        }

        [Test]
        public void NameThrowsExceptionWhenNullGiven()
        {
            RailwayStation railwayStation = null;

            Assert.Throws<ArgumentException>(() => railwayStation = new RailwayStation(null));

        }

        [Test]
        public void NameThrowsExceptionWhenWhiteSpaceGiven()
        {
            RailwayStation railwayStation = null;

            Assert.Throws<ArgumentException>(() => railwayStation = new RailwayStation(" "));

        }

        [Test]
        public void NewArrivalOnBoardShouldWork()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            railwayStation.NewArrivalOnBoard("Train1");

            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
            Assert.AreEqual("Train1", railwayStation.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasArrivedShouldWork()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            railwayStation.NewArrivalOnBoard("Train1");
            railwayStation.NewArrivalOnBoard("Train2");

            string actual = railwayStation.TrainHasArrived("Train1");
            string expected = "Train1 is on the platform and will leave in 5 minutes.";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
            Assert.AreEqual(1, railwayStation.DepartureTrains.Count);
            Assert.AreEqual("Train1", railwayStation.DepartureTrains.Peek());
            Assert.AreEqual("Train2", railwayStation.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasArrivedShouldNOTWork()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            railwayStation.NewArrivalOnBoard("Train1");
            railwayStation.NewArrivalOnBoard("Train2");

            string actual = railwayStation.TrainHasArrived("Train2");
            string expected = "There are other trains to arrive before Train2.";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2, railwayStation.ArrivalTrains.Count);
            Assert.AreEqual(0, railwayStation.DepartureTrains.Count);
            Assert.AreEqual("Train1", railwayStation.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasLeftShouldReturnTrue()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            railwayStation.NewArrivalOnBoard("Train1");
            railwayStation.NewArrivalOnBoard("Train2");

            railwayStation.TrainHasArrived("Train1");

            Assert.IsTrue(railwayStation.TrainHasLeft("Train1"));
            Assert.AreEqual(0, railwayStation.DepartureTrains.Count);
            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
            Assert.AreEqual("Train2", railwayStation.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasLeftShouldReturnFalse()
        {
            RailwayStation railwayStation = new RailwayStation("Gara Levski");

            railwayStation.NewArrivalOnBoard("Train1");
            railwayStation.NewArrivalOnBoard("Train2");

            railwayStation.TrainHasArrived("Train1");

            Assert.IsFalse(railwayStation.TrainHasLeft("Train2"));
            Assert.AreEqual(1, railwayStation.DepartureTrains.Count);
            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
        }
    }
}