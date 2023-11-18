using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void TestingSmartphoneConstructorsShouldWork()
        {
            Smartphone smartphone = new Smartphone("Nokia", 100);

            Assert.AreEqual("Nokia", smartphone.ModelName);
            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
            Assert.AreEqual(100, smartphone.MaximumBatteryCharge);
        }

        [Test]
        public void TestingShopConstructorShouldWork()
        {
            Shop shop = new Shop(10);

            Assert.AreEqual(10, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void CapacityException()
        {
            Shop shop = null;

            Assert.Throws<ArgumentException>(() => shop = new Shop(-2));
        }

        [Test]
        public void ShopAddMethodShouldWork()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("Nokia", 100);

            shop.Add(smartphone);

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void ShopAddMethodThrowExceptionWhenPhoneAlreadyExists()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("Nokia", 100);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>  shop.Add(smartphone));
            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void ShopAddMethodThrowExceptionWhenShopIsFull()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            Smartphone smartphone2 = new Smartphone("Sony", 100);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone2));
            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void ShopRemoveMethodShouldWork()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            Smartphone smartphone2 = new Smartphone("Sony", 100);

            shop.Add(smartphone);
            shop.Add(smartphone2);
            shop.Remove("Sony");

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void ShopRemoveMethodThrowExceptionWhenPhoneDoesntExist()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            Smartphone smartphone2 = new Smartphone("Sony", 100);

            shop.Add(smartphone);
            shop.Add(smartphone2);
            
            Assert.Throws<InvalidOperationException>(() => shop.Remove("Samsung"));
            Assert.AreEqual(2, shop.Count);
        }

        [Test]
        public void TestPhoneShouldReduceBatteryLevel()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);
            shop.TestPhone("Nokia", 20);

            Assert.AreEqual(80, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void TestPhoneShouldThrowExceptionWhenWrongPhoneGiven()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("IPhone", 20));
            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void TestPhoneShouldThrowExceptionWhenBatteryUsageIsMoreThanCurrentBtteryLevel()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 80);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Nokia", 90));
            Assert.AreEqual(80, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void ChargePhoneShouldWork()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);
            shop.TestPhone("Nokia", 20);
            shop.ChargePhone("Nokia");

            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void ChargePhoneShouldThrowExceptionWhenWrongPhoneGiven()
        {
            Shop shop = new Shop(5);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);
            shop.TestPhone("Nokia", 20);


            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Samsung"));
            Assert.AreEqual(80, smartphone.CurrentBateryCharge);
        }
    }
}