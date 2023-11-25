using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Helpers.Extensions;
using Library.Application.Result;
using Library.Application.Services.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using System.Reflection;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private const string BookSetMethodsPreffix = "Set";
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IMapper mapper, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddBook(BookCreateRequest bookCreateRequest)
        {
            Book book = new Book(bookCreateRequest.Title, bookCreateRequest.AuthorName,
                bookCreateRequest.Description);
            int newBookId = await _bookRepository.AddBook(book);

            return new SuccessResult<int>(newBookId);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            var books = await _bookRepository.GetBooks();
            var bookResponse = _mapper.Map<IEnumerable<BookResponse>>(books);

            return bookResponse;
        }

        public async Task<IResult> GetBookById(int id)
        {
            Book? book = await _bookRepository.GetBookById(id);

            if (book is null)
            {
                return new ErrorResult(new List<Error>()
                {
                    new Error(AppErrorCode.NotFound.ToString())
                });
            }

            var bookResponse = _mapper.Map<BookResponse>(book);
            return new SuccessResult<BookResponse>(bookResponse);
        }

        public async Task<IResult> UpdateBook(BookUpdateRequest bookUpdateRequest, int bookId)
        {
            Book? book = await _bookRepository.GetBookById(bookId);

            if (book is null) 
            {
                return new ErrorResult(new List<Error>()
                {
                    new Error(AppErrorCode.NotFound.ToString())
                });
            }

            book.UpdatePropertiesIfNotNull(bookUpdateRequest, BookSetMethodsPreffix);

            await _bookRepository.UpdateBook(book);

            var bookResponse = _mapper.Map<BookResponse>(book);
            return new SuccessResult<BookResponse>(bookResponse);
        }
    }
}
