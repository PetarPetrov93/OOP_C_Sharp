namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestingTheConstructor()
        {
            int memoryCapacity = 2;
            Device testingDevice = new Device(memoryCapacity);

            Assert.AreEqual(memoryCapacity, testingDevice.MemoryCapacity);
            Assert.AreEqual(memoryCapacity, testingDevice.AvailableMemory);
            Assert.AreEqual(0, testingDevice.Photos);
            Assert.IsNotNull(testingDevice.Applications);
        }

        [Test]
        public void TakePhotoShouldReturnTrueAndAvailableMemoryShouldBeReducedByPhotoSize()
        {
            int photoSize = 5;
            Device testingDevice = new Device(10);

            
            Assert.IsTrue(testingDevice.TakePhoto(photoSize));
            Assert.AreEqual(5, testingDevice.AvailableMemory);
            Assert.AreEqual(1, testingDevice.Photos);
        }

        [Test]
        public void TakePhotoShouldReturnFalseAndAvailableMemoryShouldNotBeChanged()
        {
            int photoSize = 5;
            Device testingDevice = new Device(3);

            Assert.AreEqual(3, testingDevice.AvailableMemory);
            Assert.IsFalse(testingDevice.TakePhoto(photoSize));
            Assert.AreEqual(0, testingDevice.Photos);
        }

        [Test]
        public void InstallAppShouldInstallAppSuccesfuly()
        {
            string app = "facebook";
            int appSize = 8;
            Device testingDevice = new Device(10);

            testingDevice.InstallApp(app, appSize);

            Assert.AreEqual(2, testingDevice.AvailableMemory);
            Assert.AreEqual(1, testingDevice.Applications.Count);
            Assert.AreEqual(app, testingDevice.Applications[0]);
            
        }

        [Test]
        public void InstallAppShouldReturnTheCorrectMessageWhenAnAppIsInstalled()
        {
            string app = "facebook";
            int appSize = 8;
            Device testingDevice = new Device(10);
            Assert.AreEqual("facebook is installed successfully. Run application?", testingDevice.InstallApp(app, appSize));

        }

        [Test]
        public void InstallAppShouldNotInstallAppWhenTheSizeIsBiggerThanTheMemory()
        {
            string app = "facebook";
            int appSize = 12;
            Device testingDevice = new Device(10);

            Assert.Throws<InvalidOperationException>(() => testingDevice.InstallApp(app, appSize));
            Assert.AreEqual(10, testingDevice.AvailableMemory);
            Assert.AreEqual(0, testingDevice.Applications.Count);
        }

        [Test]
        public void FormatDeviceShouldResetTheValuesOfPhotosApplicationsAndAvailableMemort()
        {
            Device testingDevice = new Device(10);
            testingDevice.TakePhoto(1);
            testingDevice.TakePhoto(1);
            testingDevice.TakePhoto(1);

            testingDevice.FormatDevice();

            Assert.AreEqual(0, testingDevice.Photos);
            Assert.AreEqual(10, testingDevice.AvailableMemory);
            Assert.IsNotNull(testingDevice.Applications);
        }

        [Test]
        public void ToStringMethodShouldReturnTheCorrectMessage()
        {
            Device testingDevice = new Device(10);
            testingDevice.TakePhoto(1);
            testingDevice.TakePhoto(1);
            testingDevice.InstallApp("facebook", 2);
            testingDevice.InstallApp("instagram", 1);

            Assert.AreEqual($"Memory Capacity: 10 MB, Available Memory: 5 MB{Environment.NewLine}Photos Count: 2{Environment.NewLine}Applications Installed: facebook, instagram", testingDevice.GetDeviceStatus());
        }
    }
}