using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllAuthors;
using Application.Authors.Queries.GetAuthorById;
using Application.Dtos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            _logger.LogInformation("\n\tGET ALL Authors from the Database at {Time}\n", DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new GetAuthorsQuery());

                if (operationResult.isSuccessfull)
                {

                    _logger.LogInformation("\n\t{Count} authors fetched SUCESSFULLY at {Time}\n", operationResult.Data.Count(), DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data});
                }
                else
                {
                    _logger.LogWarning("\n\tNo Authors found in the Database\n");
                    return BadRequest( new {message = operationResult.Message, operationResult.ErrorMessage});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to GET ALL authors at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation("\n\tGET Author with ID: {id} from the Database at {Time}\n",id, DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(id));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tAuthor with ID: {id} fetched SUCCESSFULLY at {Time}\n", id, DateTime.UtcNow);
                    return Ok(new {message = operationResult.Message, data = operationResult.Data});
                }
                else
                {
                    _logger.LogWarning("\n\tAuthor with ID: {id } NOT FOUND in the Database\n", id);
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to GET Author with ID: {id} from the Database at {Time}\n",id, DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorDto authorToAdd)
        {
            _logger.LogInformation("\n\tADD a new Author to the Database.\n");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var operationResult = await _mediator.Send(new CreateAuthorCommand(authorToAdd));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tADDED Author:\n\t\tID: {AuthorID}\n\t\tFirstName: {FirstName}\n\t\tLastName: {LastName}\n\tSUCCESSFULLY to the Database at {Time}.",
                                            operationResult.Data.Id, operationResult.Data.FirstName, operationResult.Data.LastName, DateTime.UtcNow);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    _logger.LogError("\n\tCouldn't ADD a new Author to the Database at {Time}\n", DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while ADDING Author to the Database at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AuthorDto authorToUpdate)
        {
            _logger.LogInformation("\n\tReceived request to UPDATE author with ID: {AuthorID} at {Time}\n", id, DateTime.UtcNow);

            if (id != authorToUpdate.Id)
            {
                return BadRequest(new { message = "Author ID mismatch." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var operationResult = await _mediator.Send(new UpdateAuthorCommand(authorToUpdate.Id, authorToUpdate.FirstName, authorToUpdate.LastName));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tAuthor with ID: {AuthorID} SUCCESSFULLY UPDATED at {Time}\n", id, DateTime.UtcNow);
                    return Ok(new {message = operationResult.Message, data = operationResult.Data});
                }
                else
                {
                    _logger.LogWarning("\n\tAuthor with ID: {AuthorID} NOT FOUND for UPDATE at {Time}\n", id, DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, errors = operationResult.ErrorMessage});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to UPDATE Author with ID: {id} at {Time}\n", id,  DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"\n\tDELETE Author with ID: {id}\n");

            try
            {
                var operationResult = await _mediator.Send(new DeleteAuthorCommand(id));

                if (operationResult.isSuccessfull)
                { 
                    _logger.LogInformation("\n\tAuthor with ID: {id} SUCCESSFULLY DELETED.\n", id);
                    return Ok(new { message = operationResult.Message, data = operationResult.Data});
                }
                else
                {
                    _logger.LogWarning("\n\tAuthor with ID: {id} NOT FOUND or could not be DELETED.\n", id);
                    return NotFound(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to DELETE Author with ID: {id} from the Database at {Time}\n",id,  DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
