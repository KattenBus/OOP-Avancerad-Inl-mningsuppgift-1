using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.Login;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllUsers")]

        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("\n\tGET ALL Users from the Database at {Time}\n", DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new GetAllUsersQuery());

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\t{COUNT} Users found in the Database at {Time}\n", operationResult.Data.Count(), DateTime.UtcNow);
                    return Ok(new {message = operationResult.Message, operationResult.Data});
                }
                else
                {
                    _logger.LogWarning("\n\tNo Users found in th Database at {Time}\n", DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to GET ALL Users from the Database at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }    
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] UserDto newUser)
        {
            _logger.LogInformation("\n\tREGISTER new User at {Time}\n", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var operationResult = await _mediator.Send(new AddNewUserCommand(newUser));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\tREGISTERED new User:\n\t\tUsername: {UserName}\n\t\tPassword: {Password}\n\tSUCCESSFULLY to the Database at {Time}.",
                                            operationResult.Data.UserName, operationResult.Data.Password, DateTime.UtcNow);
                    return Ok(new { operationResult.Message, operationResult.Data });
                }
                else
                {
                    _logger.LogError("\n\tCouldn't REGISTER new User to the Database at {Time}\n", DateTime.UtcNow);
                    return NotFound(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while trying to REGISTER new User in the Database at {Time}\n", DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] UserDto UserWantToLogIn)
        {
            _logger.LogInformation("\n\t{User} tries to LOGIN at {Time}\n", UserWantToLogIn.UserName, DateTime.UtcNow);

            try
            {
                var operationResult = await _mediator.Send(new LoginUserQuery(UserWantToLogIn));

                if (operationResult.isSuccessfull)
                {
                    _logger.LogInformation("\n\t{User} LOGGED IN SUCCESFULLY at {Time}\n", UserWantToLogIn.UserName, DateTime.UtcNow);
                    return Ok(new { Token = operationResult.Data });
                }
                else
                {
                    _logger.LogWarning("\n\t{User} LOGIN FAILED at {Time}\n", UserWantToLogIn.UserName, DateTime.UtcNow);
                    return Unauthorized(new { error = operationResult.ErrorMessage });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "\n\tUnauthorized access error occurred while {User} was attempting to do something at {Time}\n", UserWantToLogIn.UserName, DateTime.UtcNow);
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\n\tERROR occured while {USER} trying to LOGIN at {Time}\n",UserWantToLogIn.UserName, DateTime.UtcNow);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
