
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealDatabase _realDatabase;
        public UserRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public async Task<OperationResult<List<User>>> GetAllUsersList()
        {
            try
            {
                var allUsers = await _realDatabase.Users.ToListAsync();

                if (allUsers == null)
                {
                    return OperationResult<List<User>>.Failure("No Users found in the database");
                }
                else
                {
                    return OperationResult<List<User>>.Successfull(allUsers);
                }  
            }
            catch(Exception ex)
            {
                return OperationResult<List<User>>.Failure(errorMessage: $"An error occurred while fetching all Users from the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<User>> RegisterUser(User user)
        {
            try
            {
                _realDatabase.Users.Add(user);
                await _realDatabase.SaveChangesAsync();

                return OperationResult<User>.Successfull(user);
            }
            catch (Exception ex) 
            {
                return OperationResult<User>.Failure(errorMessage: $"An error occurred while registering a User from the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<User>> LogInUser(string username, string password)
        {
            var user = await _realDatabase.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return OperationResult<User>.Failure("UserName not found in the Database.");
            }
            if (user.Password != password)
            {
                return OperationResult<User>.Failure("Password is wrong.");
            }
            else
            {
                return OperationResult<User>.Successfull(user);
            }
        }
    }
}
