using Application.Dtos;
using MediatR;

namespace Application.Users.Queries.Login
{
    public class LoginUserQuery : IRequest<string>
    {
        public LoginUserQuery(UserDto loginUser) 
        {
            LoginUser = loginUser;
        }

        public UserDto LoginUser { get;}
    }
}
