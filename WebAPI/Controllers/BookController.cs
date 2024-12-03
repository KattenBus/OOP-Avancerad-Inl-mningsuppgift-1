using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Queries.GetBookById;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            try
            {
                var books = await _mediator.Send(new GetBooksQuery());
                
                if (!books.Any())
                {
                    return NotFound(new { message = "No books found in the Database."});
                }
                else
                {
                    return Ok(books);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var getBookByID = await _mediator.Send(new GetBookByIdQuery(id));

                if (getBookByID == null)
                {
                    return NotFound(new { message = $"Book with ID {id} not found." });
                }
                else
                {
                    return Ok(getBookByID);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdBook = await _mediator.Send(new CreateBookCommand(bookToAdd));

                return Ok(createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book updateRequest)
        {
            if (id != updateRequest.Id)
            {
                return BadRequest("Book ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedBook = await _mediator.Send(new UpdateBookCommand(updateRequest.Id, updateRequest.Title, updateRequest.Description));

                if (updatedBook == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(updatedBook);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteBookResult = await _mediator.Send(new DeleteBookCommand(id));

                if (deleteBookResult == null)
                {
                    return NotFound(new { message = $"Book with ID {id} not found or could not be deleted." });
                }
                else
                {
                    return Ok(new { message = $"Book with ID {id} was successfully deleted." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
