using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Users.Queries.Login
{
    public class LoginUserQuery : IRequest<OperationResult<string>>
    {
        public LoginUserQuery(UserDto loginUser) 
        {
            LoginUser = loginUser;
        }

        public UserDto LoginUser { get;}
    }
}
