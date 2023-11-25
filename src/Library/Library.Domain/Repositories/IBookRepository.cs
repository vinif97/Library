using Library.Domain.Entities;

namespace Library.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<int> AddBook(Book book);
        Task<Book?> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooks();
        Task DeleteBook(int id);
        Task UpdateBook(Book book);
    }
}
