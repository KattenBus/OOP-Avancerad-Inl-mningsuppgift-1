using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            return Ok(await _mediator.Send(new AddNewUserCommand(newUser)));
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] UserDto UserWantToLogIn)
        {
            return Ok(await _mediator.Send(new LoginUserQuery(UserWantToLogIn)));
        }
    }
}
