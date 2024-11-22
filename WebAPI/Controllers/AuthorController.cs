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
            var authors = await _mediator.Send(new GetAuthorsQuery());
            return Ok(authors); 
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var getAuthorByID = await _mediator.Send(new GetAuthorByIdQuery(id));
            return Ok(getAuthorByID);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author authorToAdd)
        {
            var createdAuthor = await _mediator.Send(new CreateAuthorCommand (authorToAdd));

            return Ok(createdAuthor);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author authorToUpdate)
        {
            if (id != authorToUpdate.Id)
            {
                return BadRequest("Author ID mismatch.");
            }

            var updatedAuthor = await _mediator.Send(new UpdateAuthorCommand(authorToUpdate.Id, authorToUpdate.FirstName, authorToUpdate.LastName));
            if (updatedAuthor == null)
            {
                return NotFound();
            }
            return Ok(updatedAuthor);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedAuthor = await _mediator.Send(new DeleteAuthorCommand(id));

            return Ok(deletedAuthor);
        }
    }
}
