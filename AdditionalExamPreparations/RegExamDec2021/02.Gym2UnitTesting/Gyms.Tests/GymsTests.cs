using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void TestingTheAthleteConstructor()
        {
            Athlete athlete = new Athlete("Petar");

            Assert.AreEqual("Petar", athlete.FullName);
            Assert.IsFalse(athlete.IsInjured);
        }

        [Test]
        public void TestingGymConstructor()
        {
            Gym gym = new Gym("The Gym", 10);

            Assert.AreEqual("The Gym", gym.Name);
            Assert.AreEqual(10, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [Test]
        public void GymNamePropertyExceptionWhenNull()
        {
            Gym gym = null;

            Assert.Throws<ArgumentNullException>(() => gym = new Gym(null, 10));
            
        }

        [Test]
        public void GymNamePropertyExceptionWhenEmpty()
        {
            Gym gym = null;

            Assert.Throws<ArgumentNullException>(() => gym = new Gym("", 10));
        }

        [Test]
        public void GymNamePropertyExceptionWhen0ShouldWork()
        {
            Gym gym = new Gym("The Gym", 0);

            Assert.AreEqual("The Gym", gym.Name);
            Assert.AreEqual(0, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [Test]
        public void GymCapacityExceptionWhenLessThan0()
        {
            Gym gym = null;

            Assert.Throws<ArgumentException>(() => gym = new Gym("The Gym", -2));
        }

        [Test]
        public void GymAddAthleteShouldWork()
        {
            Gym gym = new Gym("The Gym", 10);
            Athlete athlete = new Athlete("Petar");

            gym.AddAthlete(athlete);

            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void GymAddAthleteShouldThrowException()
        {
            Gym gym = new Gym("The Gym", 1);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete);

            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(athlete2));
            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void GymRemoveAthleteShouldWork()
        {
            Gym gym = new Gym("The Gym", 5);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            gym.RemoveAthlete("Ivan");

            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void GymRemoveAthleteShouldThrowException()
        {
            Gym gym = new Gym("The Gym", 5);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("Stoyan"));
            Assert.AreEqual(2, gym.Count);
        }

        [Test]
        public void GymInjureAthleteShouldWork()
        {
            Gym gym = new Gym("The Gym", 5);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            Athlete injuredAthlete = gym.InjureAthlete("Ivan");

            Assert.IsTrue(athlete2.IsInjured);
            Assert.IsTrue(injuredAthlete.IsInjured);
        }

        [Test]
        public void GymInjureAthleteShouldThrowException()
        {
            Gym gym = new Gym("The Gym", 5);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);
            string actual = gym.Report();
            string expected = "Active athletes at The Gym: Petar, Ivan";

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete("Stoyan"));
            Assert.AreEqual(2, gym.Count);
            Assert.AreEqual(expected, actual);
        }

        public void GymReportShouldWork()
        {
            Gym gym = new Gym("The Gym", 5);
            Athlete athlete = new Athlete("Petar");
            Athlete athlete2 = new Athlete("Ivan");
            Athlete athlete3 = new Athlete("Stoyan");

            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.InjureAthlete("Ivan");
            string actual = gym.Report();
            string expected = "Active athletes at The Gym: Petar, Stoyan";

            Assert.AreEqual(3, gym.Count);
            Assert.IsTrue(athlete2.IsInjured);
            Assert.AreEqual(expected, actual);
        }
    }
}
