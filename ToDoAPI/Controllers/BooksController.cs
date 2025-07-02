using BookStore.Application.Services;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Contracts;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _bookService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }

        //[FromBody] говорит ASP.NET Core: возьми данные из тела HTTP-запроса и распарсь их как JSON
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request) 
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Description, request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var bookId = await _bookService.CreateBook(book);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
        {
           var bookId = await _bookService.UpdateBook(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        { 
            var bookId = await _bookService.DeleteBook(id);
            return Ok(bookId);
        }

        [HttpDelete]
        public async Task DeleteAllBook()
        { 
            await _bookService.DeleteAllBook();
        }
    }
}
