using Library.Application.DTOs;
using Library.Application.Result;
using Library.Domain.Entities;
using Library.Infrastructure;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Library.Integration.Tests
{
    public class BookTests : IntegrationTestBase
    {
        private const string BookApiPath = "Book";
        private readonly JsonSerializerOptions _options;

        public BookTests(IntegrationTestFactory<Program, LibraryContext> factory) : base(factory)
        {
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [Fact]
        public async Task AddBookSuccessFullyWhenRequested()
        {
            BookCreateRequest requestBody = new("Harry Different", "Harry Valderdead");

            string payload = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(payload, Encoding.UTF8, MediaTypeJson);

            string bookId = await CreateBookTest(content);

            await GetBookByIdTest(requestBody, bookId);
        }

        [Fact]
        public async Task UpdateBookSuccessFullyWhenRequested()
        {
            BookCreateRequest requestBody = new("Harry Different", "Harry Valderdead");

            string payload = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(payload, Encoding.UTF8, MediaTypeJson);

            string bookId = await CreateBookTest(content);



            //Client.PatchAsync($"/{BookApiPath}");
        }

        [Fact]
        public async Task DeleteBookByIdSuccessFullyWhenRequested()
        {
            BookCreateRequest requestBody = new("Ordem dos Guerreiros", "Willson Zamarchi da Rosé");

            string payload = JsonSerializer.Serialize(requestBody);
            HttpContent content = new StringContent(payload, Encoding.UTF8, MediaTypeJson);

            string bookId = await CreateBookTest(content);

            await DeleteBookTest(bookId);

            await ReturnNotFoundWhenGetBookDoesntExists(bookId);
        }

        private async Task GetBookByIdTest(BookCreateRequest expectedResult, string bookId)
        {
            var response = await Client.GetAsync($"/{BookApiPath}/{bookId}");
            var result = await response.Content.ReadAsStringAsync();

            var bookResponse = JsonSerializer.Deserialize<BookResponse>(result, _options);

            Assert.NotEmpty(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(bookResponse);
            Assert.Equal(expectedResult.Title, bookResponse.Title);
            Assert.Equal(expectedResult.Description, bookResponse.Description);
            Assert.Equal(expectedResult.AuthorName, bookResponse.AuthorName);
        }

        private async Task ReturnNotFoundWhenGetBookDoesntExists(string bookId)
        {
            var response = await Client.GetAsync($"/{BookApiPath}/{bookId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<string> CreateBookTest(HttpContent content)
        {
            var response = await Client.PostAsync($"/{BookApiPath}", content);
            var result = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(result);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var bookId = JsonDocument.Parse(result).RootElement.GetProperty("data");

            return bookId.ToString();
        }

        private async Task DeleteBookTest(string bookId)
        {
            var response = await Client.DeleteAsync($"/{BookApiPath}/{bookId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
