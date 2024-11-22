using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllAuthors;
using Application.Authors.Queries.GetAuthorById;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            try
            {
                var authors = await _mediator.Send(new GetAuthorsQuery());
                if (authors == null || !authors.Any())
                {
                    return NotFound(new { message = "No authors found." });
                }
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var getAuthorByID = await _mediator.Send(new GetAuthorByIdQuery(id));

                if (getAuthorByID == null)
                {
                    return NotFound(new { message = $"Author with ID {id} not found." });
                }
                return Ok(getAuthorByID);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author authorToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAuthor = await _mediator.Send(new CreateAuthorCommand(authorToAdd));

                return Ok(createdAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author authorToUpdate)
        {
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
                var updatedAuthor = await _mediator.Send(new UpdateAuthorCommand(authorToUpdate.Id, authorToUpdate.FirstName, authorToUpdate.LastName));

                if (updatedAuthor == null)
                {
                    return NotFound(new { message = $"Author with ID {id} not found." });
                }

                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteAuthorResult = await _mediator.Send(new DeleteAuthorCommand(id));

                if (!deleteAuthorResult)
                {
                    return NotFound(new { message = $"Author with ID {id} not found or could not be deleted." });
                }

                return Ok(new { message = $"Author with ID {id} was successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
