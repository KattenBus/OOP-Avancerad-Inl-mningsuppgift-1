using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<OperationResult<List<UserDto>>>
    {

    }
}
