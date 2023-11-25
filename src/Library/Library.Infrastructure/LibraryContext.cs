using Library.Application.Helpers.Extensions;
using Library.Domain.Entities;
using Library.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
