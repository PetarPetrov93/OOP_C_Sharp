using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ConstructorShouldWorkProperly()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10,5);

            Assert.AreEqual(10, coffeeMat.WaterCapacity);
            Assert.AreEqual(5, coffeeMat.ButtonsCount);
            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void FillWaterTankShouldFillTheTank()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10, 5);

            string actialResult = coffeeMat.FillWaterTank();
            string expectedResult = "Water tank is filled with 10ml";

            Assert.AreEqual(expectedResult, actialResult);
        }

        [Test]
        public void FillWaterTankShouldNotFillTheTankWhenTankIsFull()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10, 5);
            coffeeMat.FillWaterTank();

            string actialResult = coffeeMat.FillWaterTank();
            string expectedResult = "Water tank is already full!";

            Assert.AreEqual(expectedResult, actialResult);
        }

        [Test]
        public void AddDrinkShouldAddDring()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10, 5);

            Assert.IsTrue(coffeeMat.AddDrink("Latte", 2.5));
            
        }

        [Test]
        public void AddDrinkShouldNotAddDringWhenTheCapacityIsFUll()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10, 1);
            coffeeMat.AddDrink("Mocca", 3.2);

            Assert.IsFalse(coffeeMat.AddDrink("Latte", 2.5));
        }

        [Test]
        public void AddDrinkShouldNotAddDringWhenTheDrinkIsAlreadyAdded()
        {
            CoffeeMat coffeeMat = new CoffeeMat(10, 5);
            coffeeMat.AddDrink("Mocca", 3.2);

            Assert.IsFalse(coffeeMat.AddDrink("Mocca", 2.5));
        }

        [Test]
        public void BuyDrinkShouldWorkProperly()
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, 5);
            coffeeMat.AddDrink("Mocca", 2.5);
            coffeeMat.FillWaterTank();
            string actual = coffeeMat.BuyDrink("Mocca");
            string expected = "Your bill is 2.50$";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2.5, coffeeMat.Income);
        }

        [Test]
        public void BuyDrinkShouldNotAddIncomeWhenWrongDrinkNameIsGiven()
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, 5);
            coffeeMat.AddDrink("Mocca", 2.5);
            coffeeMat.FillWaterTank();
            string actual = coffeeMat.BuyDrink("Late");
            string expected = "Late is not available!";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void BuyDrinkShouldNotAddIncomeWhenThereIsNotEnoughWater()
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, 5);
            coffeeMat.AddDrink("Mocca", 3.5);
            coffeeMat.AddDrink("Late", 2.5);
            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("Late");

            string actual = coffeeMat.BuyDrink("Mocca");
            string expected = "CoffeeMat is out of water!";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2.5, coffeeMat.Income);
        }

        [Test]
        public void CollectIncomeShouldReturnTheCorrectValueAndIncomeShouldBecome0()
        {
            CoffeeMat coffeeMat = new CoffeeMat(500, 5);
            coffeeMat.AddDrink("Mocca", 3.5);
            coffeeMat.AddDrink("Late", 2.5);
            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("Late");
            coffeeMat.BuyDrink("Mocca");
            double actual = coffeeMat.CollectIncome();
            double expected = 6;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, coffeeMat.Income);
        }

    }
}