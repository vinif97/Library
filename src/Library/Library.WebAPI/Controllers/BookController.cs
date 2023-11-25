using Library.Application.DTOs;
using Library.Application.Helpers;
using Library.Application.Result;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        { 
            _bookService = bookService;
        }
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooks()
        {
            var result = await _bookService.GetAllBooks();

            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        public async Task<ActionResult<BookResponse>> GetBookById(int id)
        {
            var result = await _bookService.GetBookById(id);

            switch (result)
            {
                case SuccessResult<BookResponse> successResult:
                    return Ok(successResult.Data);
                case ErrorResult errorResult:
                    if (errorResult.CheckIfErrorExists(AppErrorCode.NotFound))
                    {
                        return NotFound();
                    }
                    break;
            }

            throw new Exception("Unexpected result while fetching book");
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreateRequest bookCreateRequest)
        {
                var result = await _bookService.AddBook(bookCreateRequest);

            return result switch
            {
                SuccessResult<int> successResult => 
                    CreatedAtRoute("GetBook", routeValues: new { Id = successResult.Data }, value: result),
                ErrorResult errorResult => ((Func<IActionResult>)(() =>
                {
                    if (errorResult.CheckIfErrorExists(AppErrorCode.InvalidEntity))
                    {
                        return BadRequest();
                    }

                    throw new Exception("Unexpected error while adding a book");
                }))(),
                _ => throw new Exception("Unhandled type")
            };
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]BookUpdateRequest bookUpdateRequest)
        {
            var result = await _bookService.UpdateBook(bookUpdateRequest, id);

            return result switch
            {
                SuccessResult<BookResponse> successResult => ((Func<IActionResult>)(() =>
                {
                    return Ok(successResult.Data);
                }))(),
                ErrorResult errorResult => ((Func<IActionResult>)(() =>
                {
                    if (errorResult.CheckIfErrorExists(AppErrorCode.InvalidEntity))
                    {
                        return BadRequest();
                    }

                    throw new Exception("Unexpected error while adding a book");
                }))(),
                _ => throw new Exception("Unhandled type")
            };
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            await _bookService.DeleteBook(id);

            return NoContent();
        }
    }
}
