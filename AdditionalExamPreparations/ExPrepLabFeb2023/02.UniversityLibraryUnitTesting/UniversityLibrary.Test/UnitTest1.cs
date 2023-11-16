namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TheConstructorOfTheTextBookClassShouldWork()
        {
            TextBook textBook = new TextBook("300", "unknown", "novel");

            Assert.IsNotNull(textBook);
            Assert.AreEqual("300", textBook.Title);
            Assert.AreEqual("unknown", textBook.Author);
            Assert.AreEqual("novel", textBook.Category);
            Assert.IsNull(textBook.Holder);
            Assert.AreEqual(0, textBook.InventoryNumber);

            string actual = textBook.ToString();
            string expected = $"Book: 300 - 0{Environment.NewLine}Category: novel{Environment.NewLine}Author: unknown";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UniversityConstructorShouldInitializeTheListOfTextBook()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();

            Assert.IsNotNull(universityLibrary);
            Assert.IsNotNull(universityLibrary.Catalogue);
        }

        [Test]
        public void AddTextBookToLibraryShouldAddBook()
        {
            TextBook textBook = new TextBook("300", "unknown", "novel");
            UniversityLibrary universityLibrary = new UniversityLibrary();

            string actual = universityLibrary.AddTextBookToLibrary(textBook);
            string expected = $"Book: 300 - 1{Environment.NewLine}Category: novel{Environment.NewLine}Author: unknown";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(1, textBook.InventoryNumber);
            Assert.AreEqual(1, universityLibrary.Catalogue.Count);

        }

        [Test]
        public void LoanTextbookShouldChangeTheHolderName()
        {
            TextBook textBook = new TextBook("300", "unknown", "novel");
            UniversityLibrary universityLibrary = new UniversityLibrary();

            universityLibrary.AddTextBookToLibrary(textBook);

            string expected = "300 loaned to Petar.";

            string actual = universityLibrary.LoanTextBook(1, "Petar");

            Assert.AreEqual(expected , actual);
            Assert.AreEqual("Petar", textBook.Holder);
        }

        [Test]
        public void LoanTextShouldReturnTheCorrectMessageWhenTheBookIsAlreadyLoanedOut()
        {
            TextBook textBook = new TextBook("300", "unknown", "novel");
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.LoanTextBook(1, "Petar");

            string actual = universityLibrary.LoanTextBook(1, "Petar");
            string expected = "Petar still hasn't returned 300!";

            Assert.AreEqual(expected , actual);
        }

        [Test]
        public void ReturnTextBookShouldWorkProperly()
        {
            TextBook textBook = new TextBook("300", "unknown", "novel");
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.LoanTextBook(1, "Petar");

            string actual = universityLibrary.ReturnTextBook(1);
            string expected = "300 is returned to the library.";

            Assert.AreEqual(expected , actual);
            Assert.AreEqual(string.Empty, textBook.Holder);
        }


    }
}