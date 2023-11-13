namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void TestingWarriorConstructor()
        {
            Warrior warrior = new Warrior("asd", 20, 50);

            Assert.AreEqual("asd", warrior.Name);
            Assert.AreEqual(20, warrior.Damage);
            Assert.AreEqual(50, warrior.HP);
        }

        [Test]
        public void TestingNameExceptionMessage()
        {
           Assert.Throws<ArgumentException>(() => new Warrior("", 20, 50));
        }

        [Test]
        public void TestingDamageExceptionMessageWhen0IsGiven()
        {
            Assert.Throws<ArgumentException>(() => new Warrior("asd", 0, 50));
        }

        [Test]
        public void TestingDamageExceptionMessageWhenNEgativeNumberIsGiven()
        {
            Assert.Throws<ArgumentException>(() => new Warrior("asd", -5, 50));
        }

        [Test]
        public void TestingHPExceptionMessage()
        {
            Assert.Throws<ArgumentException>(() => new Warrior("asd", 20, -50));
        }

        [Test]
        public void TestingWarriorAttackWhenWarriorHPIsLowerThanMinAttackHP()
        {
            Warrior warrior = new Warrior("asd", 20, 20);
            Warrior warrior2 = new Warrior("asd", 10, 40);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));
        }

        [Test]
        public void TestingWarriorAttackWhenWarriorHPIsEqualToTheMinAttackHP()
        {
            Warrior warrior = new Warrior("asd", 20, 30);
            Warrior warrior2 = new Warrior("asd", 10, 40);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));
        }

        [Test]
        public void TestingWarriorAttackWhenAttachedWarriorHPIsLowerThanMinAttackHP()
        {
            Warrior warrior = new Warrior("asd", 20, 40);
            Warrior warrior2 = new Warrior("asd", 10, 30);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));
        }

        [Test]
        public void TestingWarriorAttackWhenAttachedWarriorHPIsEqualToTheMinAttackHP()
        {
            Warrior warrior = new Warrior("asd", 20, 40);
            Warrior warrior2 = new Warrior("asd", 10, 20);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));
        }

        [Test]
        public void TestingWarriorAttackWhenWarriorHPIsLowerThanAttachedWarriorDamage()
        {
            Warrior warrior = new Warrior("asd", 20, 40);
            Warrior warrior2 = new Warrior("asd", 50, 35);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));
        }

        [Test]
        public void TestingWarriorAttackShouldReduceHP()
        {
            Warrior warrior = new Warrior("asd", 30, 50);
            Warrior warrior2 = new Warrior("asd", 30, 35);

            warrior.Attack(warrior2);

            Assert.AreEqual(20, warrior.HP);
            Assert.AreEqual(5, warrior2.HP);
        }

        [Test]
        public void TestingWarriorAttackShouldMakeAttachedWarriorHP0()
        {
            Warrior warrior = new Warrior("asd", 40, 50);
            Warrior warrior2 = new Warrior("asd", 30, 35);

            warrior.Attack(warrior2);

            Assert.AreEqual(0, warrior2.HP);
            
        }

    }
}