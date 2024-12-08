using Application.Dtos;
using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task <OperationResult<List<User>>> GetAllUsersList();

        Task <OperationResult<User>> RegisterUser(User user);

        Task <OperationResult<User>> LogInUser(string username, string password);
    }
}
