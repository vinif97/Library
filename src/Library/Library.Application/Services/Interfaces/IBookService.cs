using Library.Application.DTOs;
using Library.Application.Result;
using Library.Domain.Entities;

namespace Library.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<IResult> AddBook(BookCreateRequest bookCreateRequest);
        Task<IEnumerable<BookResponse>> GetAllBooks();
        Task<IResult> GetBookById(int id);
        Task<IResult> UpdateBook(BookUpdateRequest bookUpdateRequest, int bookId);
        Task DeleteBook(int id);
    }
}
