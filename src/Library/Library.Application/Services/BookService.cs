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
        private readonly IImageUploadService _uploadImageService;
        private readonly IImageDeleteService _deleteImageService;

        public BookService(IMapper mapper, IBookRepository bookRepository, IImageUploadService imageUploadService,
            IImageDeleteService deleteImageService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _uploadImageService = imageUploadService;
            _deleteImageService = deleteImageService;
        }

        public async Task<IResult> AddBook(BookCreateRequest bookCreateRequest)
        {
            string? coverUrl = null;

            if (bookCreateRequest.CoverImage is not null)
            {
                coverUrl = await _uploadImageService.Upload(bookCreateRequest.CoverImage);
            }

            Book book = new Book(bookCreateRequest.Title, bookCreateRequest.AuthorName, 
                bookCreateRequest.ReleaseYear, bookCreateRequest.Description, coverUrl);

            int newBookId = await _bookRepository.AddBook(book);

            return new SuccessResult<int>(newBookId);
        }

        public async Task DeleteBook(int id)
        {
            Book? book = await _bookRepository.GetBookById(id);

            if (book is not null)
            {
                if (book.CoverUrl is not null)
                {
                    await _deleteImageService.Delete(book.CoverUrl);
                }

                await _bookRepository.DeleteBook(book);
            }
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

            if (bookUpdateRequest.CoverImage is not null)
            {
                if (!string.IsNullOrWhiteSpace(book.CoverUrl))
                {
                    await _deleteImageService.Delete(book.CoverUrl);
                }

                book.SetCoverUrl(await _uploadImageService.Upload(bookUpdateRequest.CoverImage));
            }

            await _bookRepository.UpdateBook(book);

            var bookResponse = _mapper.Map<BookResponse>(book);
            return new SuccessResult<BookResponse>(bookResponse);
        }
    }
}
