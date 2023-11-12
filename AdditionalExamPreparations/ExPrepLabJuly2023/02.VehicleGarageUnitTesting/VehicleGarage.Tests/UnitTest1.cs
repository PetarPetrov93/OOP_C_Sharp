using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VehicleConstructorShouldWorkProperly()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");

            Assert.AreEqual("Ferarri", vehicle.Brand);
            Assert.AreEqual("Enzo", vehicle.Model);
            Assert.AreEqual("BT2777KH", vehicle.LicensePlateNumber);
            Assert.IsFalse(vehicle.IsDamaged);
            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void TheGarageConstructorShouldWorkProperly()
        {
            Garage garage = new Garage(5);

            Assert.AreEqual(5, garage.Capacity);
            Assert.IsNotNull(garage.Vehicles);
            Assert.AreEqual(0, garage.Vehicles.Count);
        }

        [Test]
        public void Adding_Vehicle_Method_Should_Add_Vehicle_In_The_Garage()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);

            garage.AddVehicle(vehicle);

            Assert.AreEqual(1, garage.Vehicles.Count);
            Assert.IsTrue(garage.AddVehicle(vehicle2));

        }

        [Test]
        public void AddVehicleShouldReturnFalseWhenTheCapacityIsFull()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(1);
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle2));
            
        }

        [Test]
        public void AddVehicleShouldReturnFalseIfThereIsAlreadyAVehicleWithTheSameLicensePlateNumber()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT2777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle2));
        }

        [Test]
        public void ChargeVehiclesShouldChargeTheVehicles()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("BT7777KH", 50, false);

            Assert.AreEqual(1, garage.ChargeVehicles(90));
            Assert.AreEqual(100, vehicle2.BatteryLevel);
        }
        [Test]
        public void ChargeVehiclesShouldReturnTheCorrecCount()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("BT7777KH", 50, false);

            Assert.AreEqual(1, garage.ChargeVehicles(90));
        }

        [Test]
        public void DriveVehicleShouldDrainTheBattery()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("BT2777KH", 50, false);

            Assert.AreEqual(50, vehicle.BatteryLevel);
        }

        [Test]
        public void DrivingVehicleShouldChangeTheVehicleStatusWhenAccidentOccures()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("BT2777KH", 50, true);

            Assert.AreEqual(50, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle2.IsDamaged);
            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void DrivingVehicleShoouldNotDoAnythingWhenVehicleIsDamaged()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("BT2777KH", 50, true);
            garage.DriveVehicle("BT2777KH", 10, true);

            Assert.AreEqual(50, vehicle.BatteryLevel);

        }

        [Test]
        public void DrivingVehicleShouldNotChangeBatteryLevelWhenBatteryLevelIsLowerThanDrainage()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("BT2777KH", 50, false);
            garage.DriveVehicle("BT2777KH", 70, false);

            Assert.AreEqual(50, vehicle.BatteryLevel);
        }

        [Test]
        public void DrivingVehicleShouldNotChangeTheBatteryLevelWhenDrainageIsAbove100()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            Garage garage = new Garage(5);
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("BT2777KH", 120, false);

            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void RepairedVehiclesShouldWorkProperly()
        {
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            vehicle.IsDamaged = true;

            Vehicle vehicle2 = new Vehicle("Mercedes", "E63AMG", "BT7777KH");

            Garage garage = new Garage(5);

            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.RepairVehicles();

            Assert.IsFalse(vehicle.IsDamaged);

        }
        [Test]
        public void RepairVehicleShouldReturnTheCorrectMessage()
        {
            Garage garage = new Garage(5);
            Vehicle vehicle = new Vehicle("Ferarri", "Enzo", "BT2777KH");
            vehicle.IsDamaged = true;
            Vehicle vehicle3 = new Vehicle("Ford", "Fiesta", "BT2567KH");
            vehicle3.IsDamaged = true;
            garage.AddVehicle(vehicle3);
            garage.AddVehicle(vehicle);

            Assert.AreEqual("Vehicles repaired: 2", garage.RepairVehicles());
            Assert.IsFalse(vehicle.IsDamaged);
            Assert.IsFalse(vehicle3.IsDamaged);
        }
    }
}