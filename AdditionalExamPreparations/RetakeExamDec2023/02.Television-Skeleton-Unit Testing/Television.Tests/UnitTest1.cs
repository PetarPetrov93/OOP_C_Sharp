namespace Television.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void TestingTheConstructors()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            Assert.AreEqual("LG", tv.Brand);
            Assert.AreEqual(999.99, tv.Price);
            Assert.AreEqual(90, tv.ScreenWidth);
            Assert.AreEqual(38, tv.ScreenHeigth);
            Assert.AreEqual(0, tv.CurrentChannel);
            Assert.AreEqual(13, tv.Volume);
            Assert.IsFalse(tv.IsMuted);
        }

        [Test]
        public void SwitchOnShouldReturnTheCorrectMessage()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.SwitchOn();
            string expected = "Cahnnel 0 - Volume 13 - Sound On";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeChannelShouldThrowWhenNegativeNumGiven()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-4));
        }

        [Test]
        public void ChangeChannelShouldChangeTheChannel()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            tv.ChangeChannel(10);

            Assert.AreEqual(10, tv.CurrentChannel);
        }

        [Test]
        public void VolumeChangeUpBelow100()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.VolumeChange("UP", 37);
            string expected = "Volume: 50";

            Assert.AreEqual(50, tv.Volume);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void VolumeChangeUpAbove100ShouldReturn100()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.VolumeChange("UP", 97);
            string expected = "Volume: 100";

            Assert.AreEqual(100, tv.Volume);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void VolumeChangeDownAbove0()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.VolumeChange("DOWN", 3);
            string expected = "Volume: 10";

            Assert.AreEqual(10, tv.Volume);
            Assert.AreEqual(actual, expected);
        }

        public void VolumeChangeDownBelow0ShouldReturn0()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.VolumeChange("DOWN", 15);
            string expected = "Volume: 0";

            Assert.AreEqual(0, tv.Volume);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void MuteDeviceShouldUnmuteTheTV()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            tv.MuteDevice();

            Assert.IsFalse(tv.MuteDevice());
        }

        [Test]
        public void MuteDeviceShouldMuteTheTV()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            tv.MuteDevice();
            tv.MuteDevice();

            Assert.IsTrue(tv.MuteDevice());
        }

        [Test]
        public void TestingToStringMethod()
        {
            TelevisionDevice tv = new TelevisionDevice("LG", 999.99, 90, 38);

            string actual = tv.ToString();
            string expected = "TV Device: LG, Screen Resolution: 90x38, Price 999.99$";

            Assert.AreEqual(expected, actual);
        }
    }
}