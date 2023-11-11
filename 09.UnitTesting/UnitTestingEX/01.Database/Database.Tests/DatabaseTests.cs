namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void TestingTheConstructorOfDatabaseClass()
        {
            int[] parameters = { 1, 2, 3 };
            Database database = new Database(parameters);

            Assert.IsNotNull(database);
            Assert.AreEqual(3, database.Count);

        }

        [Test]
        public void TestingTheAddMethod()
        {
            int[] parameters = { 1, 2, 3 };
            Database database = new Database(parameters);

            database.Add(4);

            Assert.AreEqual(4, database.Count);

            for (int i = 5; i <= 16; i++)
            {
                database.Add(i);
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(17));
        }

        [Test]
        public void TestingTheRemoveMethod()
        {
            int[] parameters = { 1, 2 };
            Database database = new Database(parameters);
            database.Remove();
            database.Remove();

            Assert.AreEqual(0, database.Count);

            Assert.Throws<InvalidOperationException>(() => database.Remove());

        }

        [Test]

        public void TestingTheFetchMethod()
        {
            int[] parameters = { 1, 2 };
            Database database = new Database(parameters);

            int[] compare = {1, 2 };

            int[] fetched = database.Fetch();

            Assert.AreEqual(compare.Length, fetched.Length);
            Assert.AreEqual(compare[0], fetched[0]);
            Assert.AreEqual(compare[1], fetched[1]);
        }
    }
}
