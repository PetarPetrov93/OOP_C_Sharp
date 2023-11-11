using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FootballPlayerConstructorShouldSetTheCorrectValues()
        {
            FootballPlayer fp = new FootballPlayer("Enzo", 10, "Forward");
            FootballPlayer fp2 = new FootballPlayer("Enzo2", 1, "Goalkeeper");
            FootballPlayer fp3 = new FootballPlayer("Enzo3", 21, "Midfielder");

            Assert.AreEqual("Enzo", fp.Name);
            Assert.AreEqual(10, fp.PlayerNumber);
            Assert.AreEqual("Forward", fp.Position);
            Assert.AreEqual(0, fp.ScoredGoals);
            Assert.AreEqual("Goalkeeper", fp2.Position);
            Assert.AreEqual(1, fp2.PlayerNumber);
            Assert.AreEqual("Midfielder", fp3.Position);
            Assert.AreEqual(21, fp3.PlayerNumber);

            fp.Score();
            Assert.AreEqual(1, fp.ScoredGoals);
        }

        [Test]
        public void FootballPlayerExceptionMessages()
        {
            
            Assert.Throws<ArgumentException>(() => new FootballPlayer("Enzo", 22, "Forward"));
            Assert.Throws<ArgumentException>(() => new FootballPlayer("Enzo2", 0, "Forward"));
            Assert.Throws<ArgumentException>(() => new FootballPlayer("", 2, "Forward"));
            Assert.Throws<ArgumentException>(() => new FootballPlayer("Enzo4", 5, "Striker"));
        }

        [Test]
        public void FootballTeamConstructorShouldSetTheCorrectValues()
        {
           
            FootballTeam cfc = new FootballTeam("cfc", 17);

            Assert.AreEqual("cfc", cfc.Name);
            Assert.AreEqual(17, cfc.Capacity);
            Assert.IsNotNull(cfc.Players);
        }

        [Test]
        public void ExceptionMessagesShouldBeWorking()
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam("CFC", 10));
            Assert.Throws<ArgumentException>(() => new FootballTeam("", 20));

        }

        [Test]
        public void TestingAddNewPlayerMethodWhenThereIsSpaceForPLayers()
        {
            FootballTeam cfc = new FootballTeam("cfc", 17);

            FootballPlayer fp = new FootballPlayer("Enzo", 10, "Forward");
            FootballPlayer fp2 = new FootballPlayer("Sterling", 1, "Goalkeeper");
            FootballPlayer fp3 = new FootballPlayer("Silva", 21, "Midfielder");


            cfc.AddNewPlayer(fp);

            Assert.AreEqual(1, cfc.Players.Count);
            Assert.AreEqual("Added player Sterling in position Goalkeeper with number 1", cfc.AddNewPlayer(fp2));

        }

        [Test]
        public void TestingAddNewPlayerMethodWhenThereIsNoSpaceForPLayers()
        {
            FootballTeam cfc = new FootballTeam("cfc", 15);
            for (int i = 1; i <= 15; i++)
            {
                FootballPlayer fp = new FootballPlayer("Enzo", i, "Forward");
                cfc.AddNewPlayer(fp);
            }

            Assert.AreEqual (15, cfc.Players.Count);

            Assert.AreEqual("No more positions available!", cfc.AddNewPlayer(new FootballPlayer("Lukaku", 9, "Forward")));
        }

        [Test]
        public void PickPlayerShouldReturnThePlayerIfThereIsAnyInTheTeam()
        {
            FootballTeam cfc = new FootballTeam("cfc", 15);
            for (int i = 1; i <= 15; i++)
            {
                FootballPlayer fp = new FootballPlayer("Enzo"+i, i, "Forward");
                cfc.AddNewPlayer(fp);
            }

            FootballPlayer testPlayer = new FootballPlayer("Enzo11", 11, "Forward");

            Assert.AreEqual(testPlayer.Name, cfc.PickPlayer("Enzo11").Name);
            Assert.AreEqual(testPlayer.PlayerNumber, cfc.PickPlayer("Enzo11").PlayerNumber);
            Assert.AreEqual(testPlayer.Position, cfc.PickPlayer("Enzo11").Position);

            Assert.IsNull(cfc.PickPlayer("Terry"));
        }

        [Test]
        public void TestingThePlayerScoreMEthod()
        {
            FootballTeam cfc = new FootballTeam("cfc", 15);
            for (int i = 1; i <= 15; i++)
            {
                FootballPlayer fp = new FootballPlayer("Enzo" + i, i, "Forward");
                cfc.AddNewPlayer(fp);
            }

            cfc.PlayerScore(11);

            Assert.AreEqual(1, cfc.PickPlayer("Enzo11").ScoredGoals);

            Assert.AreEqual("Enzo11 scored and now has 2 for this season!", cfc.PlayerScore(11));
        }
    }
}