namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;
    using System.Runtime.ConstrainedExecution;

    [TestFixture]
    public class CarManagerTests
    {
        [Test]
        public void Testing_The_Constructors_When_All_Values_Are_Correct()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);

            Assert.AreEqual("Ferarri", car.Make);
            Assert.AreEqual("Enzo", car.Model);
            Assert.AreEqual(10, car.FuelConsumption);
            Assert.AreEqual(100, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
            Assert.IsNotNull(car);
        }

        [Test]
        public void Make_Should_Return_Ex_Message()
        {
            Car car = null;

            Assert.Throws<ArgumentException>(()=> car = new Car("", "Enzo", 10, 100));
            
        }

        [Test]
        public void Model_Should_Return_Ex_Message()
        {
            Car car = null;

            Assert.Throws<ArgumentException>(() => car = new Car("Ferarri", "", 10, 100));
            
        }

        [Test]
        public void FuelConsumption_Should_Return_Ex_Message()
        {
            Car car = null;

            Assert.Throws<ArgumentException>(() => car = new Car("Ferarri", "Enzo", -1, 100));
            
        }

        [Test]
        public void FuelCapacity_Should_Return_Ex_Message()
        {
            Car car = null;

            Assert.Throws<ArgumentException>(() => car = new Car("Ferarri", "Enzo", 10, -100));
            
        }
        // FuelAmmount Ex Message Cannot Be Tested As it is always auto set to 0 and then it cannot be set to be below 0

        [Test]
        public void Standard_Refuel_When_The_Refueling_Does_Not_Exceed_Fuel_Capacity()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);

            car.Refuel(50);

            Assert.AreEqual(50, car.FuelAmount);
        }

        [Test]
        public void Standard_Refuel_When_The_Refueling_Exceeds_Fuel_Capacity()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);

            car.Refuel(150);

            Assert.AreEqual(100, car.FuelAmount);
        }

        [Test]
        public void Refuel_Should_Throw_ExceptionWhenGivenNegativeArgument()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);

            Assert.Throws<ArgumentException>(() => car.Refuel(-50));
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        public void Refuel_Should_Throw_ExceptionWhenGivenZeroAsArgument()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);

            Assert.Throws<ArgumentException>(() => car.Refuel(0));
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        public void Drive_Should_Decreese_Fuel_Ammount()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);
            car.Refuel(100);
            car.Drive(100);

            Assert.AreEqual(90, car.FuelAmount);
        }

        [Test]
        public void Drive_Should_Throw_Exception()
        {
            Car car = new Car("Ferarri", "Enzo", 10, 100);
            car.Refuel(10);

            Assert.Throws<InvalidOperationException>(() => car.Drive(150));
            Assert.AreEqual(10, car.FuelAmount);
        }

    }
}