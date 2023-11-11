namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [Test]
        public void TestingPersonClassConstructor()
        {
            Person person = new Person(1111111111, "Goro");

            Assert.AreEqual(1111111111, person.Id);
            Assert.AreEqual("Goro", person.UserName);
        }

        [Test]
        public void TestingDatabaseClassConstructor()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro"),
                new Person(2, "Goro2"),
                new Person(3, "Goro3")
            };
            
            Database database = new Database(peopleToAdd);

            Assert.AreEqual(3, database.Count);
        }

        [Test]
        public void TestingTheAddRangeMethodAndItsExceptionMessage()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15"), new Person(16, "Goro16"),
                new Person(17, "Goro17")
            };
            Assert.Throws<ArgumentException>(() => new Database(peopleToAdd));
        }

        [Test]
        public void TestingTheExceptionMessagesAndFunctionalityOfAddMethod()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15")
            };
            Database database = new Database(peopleToAdd);
            Person person = new Person(16, "Goro16");
            Person person2 = new Person(17, "Goro16");
            Person person3 = new Person(16, "Goro17");
            Person person4 = new Person(17, "Goro17");

            database.Add(person);

            Assert.AreEqual(16, database.Count);
            Assert.Throws<InvalidOperationException>(() => database.Add(person2));
            Assert.Throws<InvalidOperationException>(() => database.Add(person3));
            Assert.Throws<InvalidOperationException>(() => database.Add(person3));
        }

        [Test]
        public void TestingTheAddMethodWhenItWorksProperly()
        {
            Database database = new Database();
            Person person = new Person(16, "Goro16");

            database.Add(person);

            Assert.AreEqual(1, database.Count);
            Assert.AreEqual(person.UserName, database.FindByUsername("Goro16").UserName);
            Assert.AreEqual(person.Id, database.FindByUsername("Goro16").Id);

        }

        [Test]

        public void TestingTheAddMethodWithCountMoreThan16()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15"), new Person(16, "Goro16")
            };
            Database database = new Database(peopleToAdd);

            Person newPerson = new Person(17, "Goro17");
            Assert.AreEqual(16, database.Count);
            Assert.Throws<InvalidOperationException>(() => database.Add(newPerson));
        }

        [Test]
        public void TestingTheAddMethodWithTheSameUsername()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15")
            };
            Database database = new Database(peopleToAdd);

            Person newPerson = new Person(11111, "Goro15");

            Assert.Throws<InvalidOperationException>(() => database.Add(newPerson));
        }

        [Test]
        public void TestingTheAddMethodWithTheSameId()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15")
            };
            Database database = new Database(peopleToAdd);

            Person newPerson = new Person(15, "Goro11111");

            Assert.Throws<InvalidOperationException>(() => database.Add(newPerson));
        }

        [Test]
        public void TestingTheRemoveMethodExeptionMessageAndDecreesingTheCount()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);
            database.Remove();
            Assert.AreEqual(0, database.Count);
            Assert.Throws<InvalidOperationException>(() => database.Remove());

        }

        [Test]
        public void TestingTheRemoveDecreesingCount()
        {
            Person person = new Person(16, "Goro16");
            Person person2 = new Person(15, "Goro15");
            Database database = new Database();
            database.Add(person);
            database.Add(person2);

            database.Remove();
            Assert.AreEqual(1, database.Count);
        }

        [Test]
        public void TestingFindByUsernameWhenGivenEmptyParameterShouldThrowAnException()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(""));
        }

        [Test]
        public void TestingFindByUsernameWhenDatabaseDoesNotContainTheGivenNameAsParameterShouldThrowAnException()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("Stefan"));
        }

        [Test]
        public void TestingFindByUsernameShouldReturnTheCorrectPerson()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.AreEqual(person.Id, database.FindByUsername("Goro16").Id);
            Assert.AreEqual(person.UserName, database.FindByUsername("Goro16").UserName);
        }

        [Test]
        public void TestingArgumentOutOfRangeExceptionMessageOnTheFindByIDMethodWhenParameterIsBelowZero()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
        }

        [Test]
        public void TestingInvalidOperationExceptionnMessageOnTheFindByIDMethodWhenGivenWrongID()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.Throws<InvalidOperationException>(() => database.FindById(15));
        }

        [Test]
        public void FIndByIDShouldReturnTheCorrectPerson()
        {
            Person person = new Person(16, "Goro16");
            Database database = new Database();
            database.Add(person);

            Assert.AreEqual(person.Id, database.FindById(16).Id);
            Assert.AreEqual(person.UserName, database.FindById(16).UserName);
        }

        [Test]
        public void TestingTheAddRangeMethodFunctionallity()
        {
            Person[] peopleToAdd =
            {
                new Person(1, "Goro1"),   new Person(2, "Goro2"),   new Person(3, "Goro3"),   new Person(4, "Goro4"),
                new Person(5, "Goro5"),   new Person(6, "Goro6"),   new Person(7, "Goro7"),   new Person(8, "Goro8"),
                new Person(9, "Goro9"),   new Person(10, "Goro10"), new Person(11, "Goro11"), new Person(12, "Goro12"),
                new Person(13, "Goro13"), new Person(14, "Goro14"), new Person(15, "Goro15"), new Person(16, "Goro16")
            };
            Database database = new Database(peopleToAdd);

            Assert.AreEqual(16, database.Count);
        }
    }
}