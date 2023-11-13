namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void TestingTheConstructor()
        {
            Arena arena = new Arena();

            Assert.IsNotNull(arena.Warriors);
            Assert.AreEqual(0, arena.Warriors.Count);
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollMethodShouldAddNewWarriorToTheWarriorsCollection()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("ivan", 50, 50);
            Warrior warrior2 = new Warrior("stoyan", 50, 50);
            Warrior warrior3 = new Warrior("purvan", 50, 50);

            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            arena.Enroll(warrior3);

            Assert.AreEqual(3, arena.Warriors.Count);
            Assert.AreEqual(3, arena.Count);
        }

        [Test]
        public void EnrollExceptionMessageWhenWarriorWithTheSameNameIsAlreadyAdded()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("ivan", 50, 50);
            Warrior warrior2 = new Warrior("ivan", 55, 55);

            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior2));
            Assert.AreEqual(1, arena.Warriors.Count);
            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void FightMethodShouldThrowExceptionWhenAttackersNameIsNotInTheCOllection()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("ivan", 50, 50);
            Warrior warrior2 = new Warrior("stoyan", 55, 55);
            Warrior warrior3 = new Warrior("Purvan", 54, 54);

            arena.Enroll(warrior);
            arena.Enroll(warrior2);

            Assert.Throws<InvalidOperationException>(() => arena.Fight("Purvan", "ivan"));

        }

        [Test]
        public void FightMethodShouldThrowExceptionWhenDefendersNameIsNotInTheCOllection()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("ivan", 50, 50);
            Warrior warrior2 = new Warrior("stoyan", 55, 55);
            Warrior warrior3 = new Warrior("Purvan", 54, 54);

            arena.Enroll(warrior);
            arena.Enroll(warrior2);

            Assert.Throws<InvalidOperationException>(() => arena.Fight("stoyan", "Purvan"));

        }

        [Test]
        public void FightMethodShouldWorkProperly()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("asd", 30, 50);
            Warrior warrior2 = new Warrior("asd2", 30, 35);

            arena.Enroll(warrior);
            arena.Enroll(warrior2);

            arena.Fight("asd", "asd2");

            Assert.AreEqual(20, warrior.HP);
            Assert.AreEqual(5, warrior2.HP);
        }

    }
}
