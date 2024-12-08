using Application.Interfaces.RepositoryInterfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DatabaseSeeder
{
    public class UserSeeder
    {
        private readonly IUserRepository _userRepository;

        public UserSeeder(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            var usersInDB = await _userRepository.GetAllUsersList();

            var existingUsers = usersInDB.Data ?? new List<User>();

            var testUsers = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "user1", Password = "password2135" },
                new User { Id = Guid.NewGuid(), UserName = "user2", Password = "password13214" },
                new User { Id = Guid.NewGuid(), UserName = "user3", Password = "password8344" },
                new User { Id = Guid.NewGuid(), UserName = "user4", Password = "password2182" },
                new User { Id = Guid.NewGuid(), UserName = "user5", Password = "password5677" },
                new User { Id = Guid.NewGuid(), UserName = "user6", Password = "password2356" },
                new User { Id = Guid.NewGuid(), UserName = "user7", Password = "password1934" },
                new User { Id = Guid.NewGuid(), UserName = "user8", Password = "password53425" },
                new User { Id = Guid.NewGuid(), UserName = "user9", Password = "password453" },
            };

            foreach (var testUser in testUsers)
            {
                if (!existingUsers.Any(user => user.UserName == testUser.UserName))
                {
                    await _userRepository.RegisterUser(testUser);
                }
            }
        }
    }
}
