using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SupplementsConstructorShouldWorkProperly()
        {
            Supplement supplement = new Supplement("Laser", 1111);

            Assert.AreEqual("Laser", supplement.Name);
            Assert.AreEqual(1111, supplement.InterfaceStandard);
        }

        [Test]
        public void TestingSupplementsToStringMethod()
        {
            Supplement supplement = new Supplement("Laser", 1111);

            string expectedMessage = $"Supplement: {supplement.Name} IS: {supplement.InterfaceStandard}";

            Assert.AreEqual(expectedMessage, supplement.ToString());
        }

        [Test]
        public void RobotConstructorShouldWorkProperly()
        {
            Robot robot = new Robot("LT-12", 120.5, 2);

            Assert.AreEqual("LT-12", robot.Model);
            Assert.AreEqual(120.5, robot.Price);
            Assert.AreEqual(2, robot.InterfaceStandard);
        }

        [Test]
        public void RobotSupplementsShouldBeInitialized()
        {
            Robot robot = new Robot("LT-12", 120.5, 2);

            Assert.IsNotNull(robot.Supplements);
            Assert.AreEqual(0, robot.Supplements.Count);
        }

        [Test]
        public void TestingToStringMethodOfRobot()
        {
            Robot robot = new Robot("LT-12", 120.5, 2);
            string expectedMessage = $"Robot model: {robot.Model} IS: {robot.InterfaceStandard}, Price: {robot.Price:f2}";

            Assert.AreEqual(expectedMessage, robot.ToString());
        }

        [Test]
        public void FactoryConstructorShouldWorkProperly()
        {
            Factory factory = new Factory("robotics", 5);

            Assert.AreEqual("robotics", factory.Name);
            Assert.AreEqual(5, factory.Capacity);
        }

        [Test]
        public void RobotsCollectionShouldBeInitializedInFactoryConstructor()
        {
            Factory factory = new Factory("robotics", 5);

            Assert.IsNotNull(factory.Robots);
            Assert.AreEqual(0, factory.Robots.Count);
        }

        [Test]
        public void SupplementsCollectionShouldBeInitializedInFactoryConstructor()
        {
            Factory factory = new Factory("robotics", 5);

            Assert.IsNotNull(factory.Supplements);
            Assert.AreEqual(0, factory.Supplements.Count);
        }

        [Test]
        public void ProduceRobotWhenCapacityAllowsIt()
        {
            Factory factory = new Factory("robotics", 5);

            string actualMessage = factory.ProduceRobot("LT-10", 123, 4);
            string expectedMessage = $"Produced --> Robot model: LT-10 IS: 4, Price: 123.00";

            Assert.AreEqual(1, factory.Robots.Count);
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual("LT-10", factory.Robots.First().Model);
            Assert.AreEqual(123, factory.Robots.First().Price);
            Assert.AreEqual(4, factory.Robots.First().InterfaceStandard);
        }

        [Test]
        public void ProduceMethodWhenCapacityIsFull()
        {
            Factory factory = new Factory("robotics", 2);
            
            

            factory.ProduceRobot("LT-122", 130.5, 2);
            factory.ProduceRobot("LT-152", 120.5, 2);
            string actialResult = factory.ProduceRobot("LT-222", 110.5, 2);
            string expectedResult = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(expectedResult, actialResult);
            Assert.AreEqual(2, factory.Robots.Count);
        }

        [Test]
        public void ProduceSupplement()
        {
            Factory factory = new Factory("robotics", 5);
            string actualResult = factory.ProduceSupplement("Laser", 15);
            string expectedResult = "Supplement: Laser IS: 15";

            Assert.AreEqual(expectedResult , actualResult);
        }

        [Test]
        public void UpgradeRobotShouldReturnFalseWhenThereIsAlreadyTheSameSupplementAdded()
        {
            Factory factory = new Factory("robotics", 5);
            Robot robot = new Robot("LT-12", 120.5, 1111);
            Supplement supplement = new Supplement("Laser", 1111);
            factory.UpgradeRobot(robot, supplement);
            bool isFalse = factory.UpgradeRobot(robot, supplement);
            Assert.IsFalse(isFalse);
        }

        [Test]
        public void UpgradeRobotShouldReturnFalseWhenTheStandardsAreDifferent()
        {
            Factory factory = new Factory("robotics", 5);
            Robot robot = new Robot("LT-12", 120.5, 1111);
            Supplement supplement = new Supplement("Laser", 1121);
            bool isFalse = factory.UpgradeRobot(robot, supplement);
            Assert.IsFalse(isFalse);
        }

        [Test]
        public void UpgradeRobotShouldReturnTrue()
        {
            Factory factory = new Factory("robotics", 5);
            Robot robot = new Robot("LT-12", 120.5, 1111);
            Supplement supplement = new Supplement("Laser", 1111);
            bool isTrue = factory.UpgradeRobot(robot, supplement);
            Assert.IsTrue(isTrue);
        }

        [Test]
        public void SellRobotShouldSellTheCorrectRobot()
        {
            Factory factory = new Factory("robotics", 5);
            Robot robot = new Robot("LT-12", 170.5, 1111);
            Robot robot2 = new Robot("LT-22", 130, 1111);
            Robot robot3 = new Robot("LT-32", 150, 1111);
            factory.Robots.Add(robot);
            factory.Robots.Add(robot2);
            factory.Robots.Add(robot3);

            Assert.AreEqual(robot3.Model, factory.SellRobot(165).Model);
            Assert.AreEqual(robot3.Price, factory.SellRobot(165).Price);
            Assert.AreEqual(robot3.InterfaceStandard, factory.SellRobot(165).InterfaceStandard);
        }

        [Test]
        public void TestingTheRobotsCollectionInTheFactory()
        {
            Factory factory = new Factory("robotics", 5);
            Robot robot = new Robot("LT-12", 170.5, 1111);
            Robot robot2 = new Robot("LT-22", 130, 1111);
            Robot robot3 = new Robot("LT-32", 150, 1111);
            factory.Robots.Add(robot);
            factory.Robots.Add(robot2);
            factory.Robots.Add(robot3);

            Assert.AreEqual(3, factory.Robots.Count);
        }

        [Test]
        public void TestingTheRobotsCollectionInTheFactoryWhenCapacityIsFull()
        {
            Factory factory = new Factory("robotics", 2);
            Robot robot = new Robot("LT-12", 170.5, 1111);
            Robot robot2 = new Robot("LT-22", 130, 1111);
            Robot robot3 = new Robot("LT-32", 150, 1111);
            factory.Robots.Add(robot);
            factory.Robots.Add(robot2);
            factory.Robots.Add(robot3);

            Assert.AreEqual(3, factory.Robots.Count);
        }

        [Test]
        public void TestingSupplementsCollectionInTheFacotry()
        {
            Factory factory = new Factory("robotics", 2);
            Supplement supplement = new Supplement("LK", 11);
            Supplement supplement2 = new Supplement("L2K", 113);
            Supplement supplement3 = new Supplement("L3K", 112);
            factory.Supplements.Add(supplement);
            factory.Supplements.Add(supplement2);
            factory.Supplements.Add(supplement3);

            Assert.AreEqual(3, factory.Supplements.Count);
        }
    }
}