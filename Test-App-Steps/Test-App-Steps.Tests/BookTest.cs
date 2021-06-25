using System;
using Xunit;
using Test_App_Steps.Server.Models;

namespace Test_App_Steps.Tests
{
    public class BookTest
    {
        [Fact]
        public void CanchangeBookName()
        {
            var b = new Book
            {
                bookName="testBook1",
                bookAuthor = "AuthorBookTest1",
                bookType = "romantique",
            };

            b.bookName = "New Name";

            //Assert
            Assert.Equal("New Name", b.bookName);

        }



        [Fact]
        public void CanchangeAuthor()
        {
            var b = new Author
            {
                authorName = "Dali moncef",
                 
            };

            b.authorName = "Dali moncef";

            //Assert
            Assert.Equal("Dali moncef", b.authorName);

        }




        [Fact]
        public void CanchangeTypeBook()
        {
            var b = new TypeBook
            {
                typeName = "historique",

            };

            b.typeName = "historique";

            //Assert
            Assert.Equal("historique", b.typeName);

        }















    }
}
