using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void CarConstructorShouldWorkFine()
            {
                Car car = new Car("Ferarri", 3);

                Assert.AreEqual("Ferarri", car.CarModel);
                Assert.AreEqual(3, car.NumberOfIssues);
                Assert.IsFalse(car.IsFixed);
            }

            [Test]
            public void GarageConstructorShouldWorkFine()
            {
                Garage garage = new Garage("ASD", 2);

                Assert.AreEqual("ASD", garage.Name);
                Assert.AreEqual(2, garage.MechanicsAvailable);
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            
            [Test]
            public void GarageNameExceptionMessageWhenEmptyStringGiven()
            {
                Garage garage = null;
                Assert.Throws<ArgumentNullException>(() => garage = new Garage("", 2));
            }

            [Test]
            public void GarageNameExceptionMessageWhenNullGiven()
            {
                Garage garage = null;
                Assert.Throws<ArgumentNullException>(() => garage = new Garage(null, 2));
            }

            [Test]
            public void GarageMechanicsAvailableExceptionMessageWhen0()
            {
                Garage garage = null;
                Assert.Throws<ArgumentException>(() => garage = new Garage("ASD", 0));
            }

            [Test]
            public void GarageMechanicsAvailableExceptionMessageWhenNegative()
            {
                Garage garage = null;
                Assert.Throws<ArgumentException>(() => garage = new Garage("ASD", -3));
            }

            [Test]
            public void AddCarrShouldAddCar()
            {
                Car car = new Car("Ferarri", 3);
                Garage garage = new Garage("ASD", 2);
                garage.AddCar(car);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void AddCarrExceptonWhenNoMechanicIsAvailable()
            {
                Car car = new Car("Ferarri", 3);
                Car car2 = new Car("Ferarri2", 2);
                Garage garage = new Garage("ASD", 1);
                garage.AddCar(car);

                Assert.Throws<InvalidOperationException>(() => garage.AddCar(car2));
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void FixCarShouldRepairTheCar()
            {
                Car car = new Car("Ferarri", 3);
                Garage garage = new Garage("ASD", 1);
                garage.AddCar(car);

                Car fixedCar = garage.FixCar("Ferarri");

                Assert.AreEqual(car.CarModel, fixedCar.CarModel);
                Assert.AreEqual(0, fixedCar.NumberOfIssues);
            }

            [Test]
            public void FixCarShouldThrowExceptionWhenCarDoesntExist()
            {
                Car car = new Car("Ferarri", 3);
                Garage garage = new Garage("ASD", 1);
                garage.AddCar(car);

                Assert.Throws<InvalidOperationException>(() => garage.FixCar("Ferarri2"));

            }

            [Test]
            public void RemoveFixedCarShouldWork()
            {
                Car car = new Car("Ferarri", 3);
                Car car2 = new Car("Ferarri2", 2);
                Car car3 = new Car("Ferarri3", 1);
                Garage garage = new Garage("ASD", 5);
                garage.AddCar(car);
                garage.AddCar(car2);
                garage.AddCar(car3);
                garage.FixCar("Ferarri");
                garage.FixCar("Ferarri2");

                int expected = garage.RemoveFixedCar();

                Assert.AreEqual(2, expected);
                Assert.AreEqual(1, garage.CarsInGarage);

            }

            [Test]
            public void RemoveFixedCarShouldThrowException()
            {
                Car car = new Car("Ferarri", 3);
                Car car2 = new Car("Ferarri2", 2);
                Car car3 = new Car("Ferarri3", 1);
                Garage garage = new Garage("ASD", 5);
                garage.AddCar(car);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());

                Assert.AreEqual(3, garage.CarsInGarage);

            }

            [Test]
            public void ReportMethodShouldWork()
            {
                Car car = new Car("Ferarri", 3);
                Car car2 = new Car("Ferarri2", 2);
                Car car3 = new Car("Ferarri3", 1);
                Garage garage = new Garage("ASD", 5);
                garage.AddCar(car);
                garage.AddCar(car2);
                garage.AddCar(car3);
                garage.FixCar("Ferarri3");

                string expected = $"There are 2 which are not fixed: Ferarri, Ferarri2.";
                string actual = garage.Report();

                Assert.AreEqual(expected, actual);
                Assert.AreEqual(3, garage.CarsInGarage);
                Assert.IsTrue(car3.IsFixed);
                Assert.IsFalse(car2.IsFixed);
                Assert.IsFalse(car.IsFixed);
            }

        }
    }
}