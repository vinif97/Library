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
        public void UpdatePropertiesSuccessfullyWhenValid()
        { 
            Book book = new Book("Test Name", "Wrong name");
            BookUpdateRequest updateRequest = new BookUpdateRequest() 
            {
                Title= "New and Better Title",
                AuthorName = "New Better Author Name",
                Description = "Now this is a description"
            };
            Author author = new Author(updateRequest.AuthorName);

            book.UpdatePropertiesIfNotNull(updateRequest, SetMethodsPreffix);

            Assert.Equal(updateRequest.Title, book.Title);
            Assert.Equal(author.FirstName, book.Author.FirstName);
            Assert.Equal(author.Surname, book.Author.Surname);
            Assert.Equal(updateRequest.Description, book.Description);
        }

        [Fact]
        public void DontUpdatePropertiesWhenNull()
        {
            string expectedTitle = "New and Better Title";
            string expectedaAuthorName = "Amanda França de Oliveira";

            Book book = new Book("Test Title", expectedaAuthorName);
            BookUpdateRequest updateRequest = new BookUpdateRequest()
            {
                Title = "New and Better Title",
                AuthorName = null,
                Description = null
            };

            book.UpdatePropertiesIfNotNull(updateRequest, SetMethodsPreffix);

            Assert.Equal(expectedTitle, book.Title);
            Assert.Equal(expectedaAuthorName, book.Author.FullName);
        }
    }
}
