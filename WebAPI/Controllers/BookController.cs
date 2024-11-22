using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Queries.GetBookById;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public BookController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _mediatr.Send(new GetBooksQuery());
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var getBookByID = await _mediatr.Send(new GetBookByIdQuery(id));

            return Ok(getBookByID);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book bookToAdd)
        {
            var createdBook = await _mediatr.Send(new CreateBookCommand(bookToAdd));
            return Ok(createdBook);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book updateRequest)
        {
            if (id != updateRequest.Id)
                return BadRequest("Book ID mismatch.");

            var updatedBook = await _mediatr.Send(new UpdateBookCommand(updateRequest.Id, updateRequest.Title, updateRequest.Description));
            if (updatedBook == null)
            {
                return NotFound();
            }
                
            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBook = await _mediatr.Send(new DeleteBookCommand(id));

            return Ok(deletedBook);
        }
    }
}
