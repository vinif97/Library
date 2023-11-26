using Library.Application.DTOs;
using Library.Application.Helpers.Extensions;
using Library.Domain.Entities;
using Library.Domain.ValueObjects;

namespace Library.Application.Tests.Helpers
{
    public class ObjectExtensionTests
    {
        private const string SetMethodsPreffix = "Set";

        [Fact]
        public void UpdatePropertiesSuccessfully_WhenValid()
        { 
            Book book = new Book("Test Name", "Wrong name", 1950);
            BookUpdateRequest updateRequest = new BookUpdateRequest() 
            {
                Title= "New and Better Title",
                AuthorName = "New Better Author Name",
                Description = "Now this is a description",
                ReleaseYear = 2006
            };
            Author author = new Author(updateRequest.AuthorName);

            book.UpdatePropertiesIfNotNull(updateRequest, SetMethodsPreffix);

            Assert.Equal(updateRequest.Title, book.Title);
            Assert.Equal(author.FirstName, book.Author.FirstName);
            Assert.Equal(author.Surname, book.Author.Surname);
            Assert.Equal(updateRequest.Description, book.Description);
            Assert.Equal(updateRequest.ReleaseYear, book.ReleaseYear);
        }

        [Fact]
        public void DontUpdateProperties_WhenNull()
        {
            string expectedTitle = "New and Better Title";
            string expectedaAuthorName = "Amanda França de Oliveira";
            int expectedReleaseYear = 2015;

            Book book = new Book("Test Title", expectedaAuthorName, expectedReleaseYear);
            BookUpdateRequest updateRequest = new BookUpdateRequest()
            {
                Title = "New and Better Title",
                AuthorName = null,
                Description = null,
                ReleaseYear = null
            };

            book.UpdatePropertiesIfNotNull(updateRequest, SetMethodsPreffix);

            Assert.Equal(expectedTitle, book.Title);
            Assert.Equal(expectedaAuthorName, book.Author.FullName);
            Assert.Equal(expectedReleaseYear, book.ReleaseYear);
        }
    }
}
