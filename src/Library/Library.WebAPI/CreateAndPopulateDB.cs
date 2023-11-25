using Library.Domain.Entities;
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI
{
    public static class CreateAndPopulateDB
    {
        public static void CreateDB(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            // Not a good ideia to use in production, but I'll go with this for this project
            dbContext.Database.Migrate();
        }

        public static void PopulateDB(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();

            if (!dbContext.Books.Any())
            {
                dbContext.Books.Add(new Book("Weird Warriors", "Vagner Zamarchi", 2005));
                dbContext.Books.Add(new Book("Weird Rangers", "Vagner Zamarchi", 2008));
                dbContext.Books.Add(new Book("Melon's Diary", "Wilson Willverbh", 2002));
                dbContext.Books.Add(new Book("Crazy Peopl", "Vinicius França de Oliveira", 2020));
                dbContext.Books.Add(new Book("Beautiful Flowers", "Samantha Rosa", 2015));

                dbContext.SaveChanges();
            }
        }
    }
}
