using Library.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Testcontainers.MsSql;

namespace Library.Integration.Tests
{
    public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
        where TProgram : class where TDbContext : DbContext
    {
        private readonly MsSqlContainer _dbContainer;

        public IntegrationTestFactory()
        {
            _dbContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("Passw0rd@")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var contextOptions = services.SingleOrDefault(s =>
                    s.ServiceType == typeof(DbContextOptions<TDbContext>));

                if (contextOptions is not null)
                {
                    services.Remove(contextOptions);
                }

                services.AddDbContext<TDbContext>(options => { options.UseSqlServer(_dbContainer.GetConnectionString()); });
            });
        }

        public async Task InitializeAsync() => await _dbContainer.StartAsync();

        public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
    }
}