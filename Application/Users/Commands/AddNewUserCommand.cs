using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Users.Commands
{
    public class AddNewUserCommand : IRequest<OperationResult<UserDto>>
    {
        public AddNewUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
        public UserDto NewUser { get;}
    }
}
