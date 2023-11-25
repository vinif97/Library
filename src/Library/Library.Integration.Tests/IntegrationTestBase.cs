using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Integration.Tests
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, LibraryContext>>
    {
        protected readonly IntegrationTestFactory<Program, LibraryContext> Factory;
        protected readonly HttpClient Client;
        protected readonly LibraryContext DbContext;
        protected const string MediaTypeJson = "application/json";
        protected readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public IntegrationTestBase(IntegrationTestFactory<Program, LibraryContext> factory)
        {
            Factory = factory;

            Client = Factory.CreateClient();

            var scope = factory.Services.CreateScope();

            DbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            DbContext.Database.Migrate();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
