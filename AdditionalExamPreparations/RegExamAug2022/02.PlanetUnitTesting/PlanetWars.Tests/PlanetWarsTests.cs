using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void WeaponConstructorShouldWorkFine()
            {
                Weapon weapon = new Weapon("nuce", 12.5, 15);

                Assert.AreEqual("nuce", weapon.Name);
                Assert.AreEqual(12.5, weapon.Price);
                Assert.AreEqual(15, weapon.DestructionLevel);
                Assert.IsTrue(weapon.IsNuclear);
            }

            [Test]
            public void WeaponPriceBelow0Exception()
            {
                Weapon weapon = null;
                Assert.Throws<ArgumentException>(() => weapon = new Weapon("nuce", -12.5, 15));
            }

            [Test]
            public void IsNuclearShouldReturnFalse()
            {
                Weapon weapon = new Weapon("nuce", 12.5, 5);
                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]
            public void IncreaseDesructionLevelShouldChangeIsNuclearWhen9Becomes10()
            {
                Weapon weapon = new Weapon("nuce", 12.5, 9);

                weapon.IncreaseDestructionLevel();
                Assert.IsTrue(weapon.IsNuclear);
                Assert.AreEqual(10, weapon.DestructionLevel);
            }

            [Test]
            public void IncreaseDestructionLevelShouldIncreaseBy1()
            {
                Weapon weapon = new Weapon("nuce", 12.5, 5);

                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(6, weapon.DestructionLevel);
            }

            [Test]
            public void TestingThePlanetConstructor()
            {
                Planet planet = new Planet("B5", 10.5);

                Assert.AreEqual("B5", planet.Name);
                Assert.AreEqual(10.5, planet.Budget);
                Assert.AreEqual(0, planet.Weapons.Count);
                Assert.AreEqual(0, planet.MilitaryPowerRatio);
                Assert.IsNotNull(planet.Weapons);
            }

            [Test]
            public void ProfitShouldIncreaseBudget()
            {
                Planet planet = new Planet("B5", 10.5);
                planet.Profit(10);

                Assert.AreEqual(20.5, planet.Budget);
            }

            [Test]
            public void SpendFundsShouldDecreaseBudget()
            {
                Planet planet = new Planet("B5", 10.5);
                planet.SpendFunds(5);

                Assert.AreEqual(5.5, planet.Budget);
            }

            [Test]
            public void SpendFundsShouldThrowException()
            {
                Planet planet = new Planet("B5", 10.5);

                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(15));
                Assert.AreEqual(10.5, planet.Budget);
            }

            [Test]
            public void AddWeaponShouldAddWeapon()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                planet.AddWeapon(weapon);

                Assert.AreEqual(1, planet.Weapons.Count);
                Assert.AreEqual(5, planet.MilitaryPowerRatio);
                Assert.AreEqual(weapon.Name, planet.Weapons.First().Name);
            }

            [Test]
            public void AddWeaponShouldThrowException()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                Weapon weapon2 = new Weapon("nuce", 4.5, 7);
                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(() =>  planet.AddWeapon(weapon2));
                Assert.AreEqual(1, planet.Weapons.Count);
                Assert.AreEqual(5, planet.MilitaryPowerRatio);

            }

            [Test]
            public void RemoveWeaponShouldRemoveWeapon()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                planet.AddWeapon(weapon);
                planet.RemoveWeapon("nuce");

                Assert.AreEqual(planet.Weapons.Count, 0);

            }

            [Test]
            public void RemoveWeaponShouldNotRemoveWeapon()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                planet.AddWeapon(weapon);
                planet.RemoveWeapon("bomb");

                Assert.AreEqual(planet.Weapons.Count, 1);

            }

            [Test]
            public void UpgradeWeaponShouldWork()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon("nuce");

                Assert.AreEqual(6, planet.Weapons.First().DestructionLevel);

            }

            [Test]
            public void UpgradeWeaponShouldThrowException()
            {
                Planet planet = new Planet("B5", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("bomb"));
                Assert.AreEqual(5, planet.Weapons.First().DestructionLevel);

            }

            [Test]
            public void DestructOpponentSouldWork()
            {
                Planet planet = new Planet("B5", 10.5);
                Planet planet2 = new Planet("B4", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                Weapon weapon2 = new Weapon("nuce2", 2.5, 3);
                planet.AddWeapon(weapon);
                planet2.AddWeapon(weapon2);

                string actual = planet.DestructOpponent(planet2);
                string expected = "B4 is destructed!";

                Assert.AreEqual(expected, actual);

            }

            [Test]
            public void DestructOpponentSouldThrowException()
            {
                Planet planet = new Planet("B5", 10.5);
                Planet planet2 = new Planet("B4", 10.5);
                Weapon weapon = new Weapon("nuce", 2.5, 5);
                Weapon weapon2 = new Weapon("nuce2", 2.5, 7);
                planet.AddWeapon(weapon);
                planet2.AddWeapon(weapon2);

                Assert.Throws<InvalidOperationException>(() =>  planet.DestructOpponent(planet2));

            }

        }
    }
}
