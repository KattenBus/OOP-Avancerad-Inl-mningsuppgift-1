using Domain;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {

    }
}
