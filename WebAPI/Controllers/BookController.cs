using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Queries.GetBookById;
using Application.Dtos;
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
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<BookController>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get()
        {
            _logger.LogInformation("\n\tGET ALL Books from the Database at {Time}\n", DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new GetBooksQuery());
                
                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\t{COUNT} Books found in the Database at {Time}\n", operationResult.Data.Count(), DateTime.UtcNow);
                    return Ok(new {message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogWarning("\n\tNo books found in the Database at {Time}\n", DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, operationResult.ErrorMessage});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to GET ALL authors at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation("\n\tGET Book with ID: {id} at {Time}\n", id, DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new GetBookByIdQuery(id));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tBook with ID: {id} fetched SUCCESSFULLY at {Time}\n", id, DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogWarning("\n\tBook with ID: {id} NOT FOUND at {Time}\n", id, DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to GET Book with ID: {id} from the Database at {Time}\n", id, DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDto bookToAdd)
        {
            _logger.LogInformation("\n\tADD a new Book to the Database.\n");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var operationResult = await _mediator.Send(new CreateBookCommand(bookToAdd));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tADDED Book:\n\t\tID: {BookID}\n\t\tTitle: {BookTitle}\n\t\tDescription: {BookDescription}\n\tSUCCESSFULLY to the Database at {Time}.",
                            operationResult.Data.Id, operationResult.Data.Title, operationResult.Data.Description, DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogError("\n\tCouldn't ADD a new Book to the Database at {Time}\n", DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to ADD Book to the Database at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BookDto updateRequest)
        {
            _logger.LogInformation("\n\tUPDATE Book with ID: {id} at {Time}\n", id, DateTime.UtcNow);

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
                var operationResult = await _mediator.Send(new UpdateBookCommand(updateRequest.Id, updateRequest.Title, updateRequest.Description));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tBook with ID: {id} SUCCESSFULLY UPDATED at {Time}\n", id, DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogWarning("\n\tBook with ID: {id} NOT FOUND at {Time}\n", id, DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to UPDATE Book with ID: {id} at {Time}\n",id,  DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("\n\tDELETE Book with ID: {id} at {Time}\n", id, DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new DeleteBookCommand(id));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tBook with ID: {id} SUCCESFULLY DELETED at {Time}\n", id, DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogWarning("\n\tBook with ID: {id} NOT FOUND in the Database at {Time}\n", id, DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to DELETE Book with ID: {id} at {Time}\n",id,  DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
