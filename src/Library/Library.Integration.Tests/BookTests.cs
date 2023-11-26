using Library.Application.DTOs;
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
        public async Task AddBookSuccessfullyWithImage_WhenRequestIsValid()
        {
            BookCreateRequest requestBody = new("Harry Different", "Harry Valderdead", 2022);

            string PathToFile = Path.Combine(BaseDirectory, "Data\\Images\\testImage.png");
            using var fileToUpload = File.OpenRead(PathToFile);
            using var imageContent = new StreamContent(fileToUpload);

            var formData = new MultipartFormDataContent()
            {
                { new StringContent(requestBody.Title), nameof(BookCreateRequest.Title) },
                { new StringContent(requestBody.AuthorName), nameof(BookCreateRequest.AuthorName) },
                { new StringContent(requestBody.ReleaseYear.ToString()), nameof(BookCreateRequest.ReleaseYear) },
                { imageContent, nameof(BookCreateRequest.File), "testImage.png" }
            };

            string bookId = await CreateBookTest(formData);

            await GetBookByIdTest(requestBody, bookId);
        }

        [Fact]
        public async Task UpdateBookSuccessfully_WhenRequestIsValid()
        {
            BookCreateRequest requestBody = new("Harry The Same", "Amanda Oliveira", 1999);

            string PathToFile = Path.Combine(BaseDirectory, "Data\\Images\\testImage.png");
            using var fileToUpload = File.OpenRead(PathToFile);
            using var imageContent = new StreamContent(fileToUpload);

            var formData = new MultipartFormDataContent()
            {
                { new StringContent(requestBody.Title), nameof(BookCreateRequest.Title) },
                { new StringContent(requestBody.AuthorName), nameof(BookCreateRequest.AuthorName) },
                { new StringContent(requestBody.ReleaseYear.ToString()), nameof(BookCreateRequest.ReleaseYear) }
            };

            string bookId = await CreateBookTest(formData);

            BookUpdateRequest requestUpdateBody = new()
            {
                Title = "Vinicius Magic",
                AuthorName = "Vinicius França de Oliveira",
                Description = "A Short Description"
            };

            formData = new MultipartFormDataContent()
            {
                { new StringContent(requestUpdateBody.Title), nameof(BookCreateRequest.Title) },
                { new StringContent(requestUpdateBody.AuthorName), nameof(BookCreateRequest.AuthorName) },
                { new StringContent(requestUpdateBody.Description.ToString()), nameof(BookCreateRequest.ReleaseYear) }
            };

            await UpdateBookTest(requestUpdateBody, formData, bookId);
        }

        [Fact]
        public async Task DeleteBookByIdSuccessfully_WhenRequestIsValid()
        {
            BookCreateRequest requestBody = new("Ordem dos Guerreiros", "Willson Zamarchi da Rosé", 2005);

            var formData = new MultipartFormDataContent()
            {
                { new StringContent(requestBody.Title), nameof(BookCreateRequest.Title) },
                { new StringContent(requestBody.AuthorName), nameof(BookCreateRequest.AuthorName) },
                { new StringContent(requestBody.ReleaseYear.ToString()), nameof(BookCreateRequest.ReleaseYear) }
            };

            string bookId = await CreateBookTest(formData);

            await DeleteBookTest(bookId);

            await ReturnNotFoundWhenGetBookDoesntExists(bookId);
        }

        [Fact]
        async Task UpdateImageSuccessfully_WhenImageIsSent()
        {
            BookCreateRequest requestBody = new("Harry Different", "Harry Valderdead", 2022);

            var formData = new MultipartFormDataContent()
            {
                { new StringContent(requestBody.Title), nameof(BookCreateRequest.Title) },
                { new StringContent(requestBody.AuthorName), nameof(BookCreateRequest.AuthorName) },
                { new StringContent(requestBody.ReleaseYear.ToString()), nameof(BookCreateRequest.ReleaseYear) },
            };

            string bookId = await CreateBookTest(formData);

            string PathToFile = Path.Combine(BaseDirectory, "Data\\Images\\testImage.png");
            using var fileToUpload = File.OpenRead(PathToFile);
            using var imageContent = new StreamContent(fileToUpload);

            formData = new MultipartFormDataContent()
            {
                { imageContent, nameof(BookCreateRequest.File), "testImage.png" }
            };

            await UpdateBookImageTest(formData, bookId);
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

        private async Task UpdateBookTest(BookUpdateRequest expectedResult, HttpContent content, string bookId)
        {
            var response = await Client.PatchAsync($"/{BookApiPath}/{bookId}", content);
            var result = await response.Content.ReadAsStringAsync();
            var bookResponse = JsonSerializer.Deserialize<BookResponse>(result, _options);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(bookResponse);
            Assert.Equal(expectedResult.Title, bookResponse.Title);
            Assert.Equal(expectedResult.Description, bookResponse.Description);
            Assert.Equal(expectedResult.AuthorName, bookResponse.AuthorName);
        }

        private async Task UpdateBookImageTest(HttpContent content, string bookId)
        {
            var response = await Client.PatchAsync($"/{BookApiPath}/{bookId}", content);
            var result = await response.Content.ReadAsStringAsync();
            var bookResponse = JsonSerializer.Deserialize<BookResponse>(result, _options);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(bookResponse);
            Assert.NotNull(bookResponse.CoverUrl);
        }
    }
}
