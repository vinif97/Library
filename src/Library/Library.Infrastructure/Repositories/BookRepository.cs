using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) 
        {
            _context = context;
        }

        public async Task<int> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.BookId;
        }

        public async Task<Book?> GetBookById(int id)
        {
            var book = await _context.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await _context.Books.AsNoTracking().ToListAsync();
            return books;
        }

        public async Task DeleteBook(int id)
        {
            Book book = new Book();
            book.SetBookId(id);
            _context.Books.Attach(book);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
