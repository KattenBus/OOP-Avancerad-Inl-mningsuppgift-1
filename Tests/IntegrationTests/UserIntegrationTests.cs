using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Commands;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.Login.Helpers;
using Application.Users.Queries.Login;
using Domain;
using FakeItEasy;
using Microsoft.Extensions.Configuration;

namespace Tests.IntegrationTests
{
    public class UserIntegrationTests
    {
        [Test]
        public async Task GetAllUsersQueryHandler_ReturnsListOfUserDtos_WhenUsersExist()
        {
            // Arrange
            var mockRepository = A.Fake<IUserRepository>();

            var usersList = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "user1", Password = "password1" },
                new User { Id = Guid.NewGuid(), UserName = "user2", Password = "password2" }
            };

            A.CallTo(() => mockRepository.GetAllUsersList())
                .Returns(Task.FromResult(OperationResult<List<User>>.Successfull(usersList)));

            var query = new GetAllUsersQuery();
            var handler = new GetAllUsersQueryHandler(mockRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data);
            Assert.That(usersList.Count, Is.EqualTo(result.Data.Count));

            for (int index = 0; index < usersList.Count; index++)
            {
                Assert.That(usersList[index].UserName, Is.EqualTo(result.Data[index].UserName));
                Assert.That(usersList[index].Password, Is.EqualTo(result.Data[index].Password));
            }
        }

        [Test]
        public async Task AddNewUserCommandHandler_ReturnsUserDto_WhenUserIsAdded()
        {
            // Arrange
            var newUserDto = new UserDto { UserName = "testuser", Password = "password123" };
            var mockRepository = A.Fake<IUserRepository>();
            var newUserToAdd = new User
            {
                Id = Guid.NewGuid(),
                UserName = newUserDto.UserName,
                Password = newUserDto.Password
            };

            A.CallTo(() => mockRepository.RegisterUser(A<User>.That.Matches(user =>
                user.UserName == newUserDto.UserName && user.Password == newUserDto.Password)))
                .Returns(Task.FromResult(OperationResult<User>.Successfull(newUserToAdd)));

            var command = new AddNewUserCommand(newUserDto);
            var handler = new AddNewUserCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(newUserDto.UserName, Is.EqualTo(result.Data.UserName));
            Assert.That(newUserDto.Password, Is.EqualTo(result.Data.Password));
        }

        [Test]
        public async Task LoginUserQueryHandler_ReturnsToken_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginUserDto = new UserDto { UserName = "testuser", Password = "password123" };
            var mockRepository = A.Fake<IUserRepository>();

            var mockConfiguration = A.Fake<IConfiguration>();
            A.CallTo(() => mockConfiguration["JwtSettings:SecretKey"]).Returns("simons_super_secret_key_that_has_to_be_atleast_32_characters_long");

            var tokenHelper = new TokenHelper(mockConfiguration);

            var userFromDB = new User
            {
                Id = Guid.NewGuid(),
                UserName = loginUserDto.UserName,
                Password = loginUserDto.Password
            };
            A.CallTo(() => mockRepository.LogInUser(loginUserDto.UserName, loginUserDto.Password))
                .Returns(Task.FromResult(OperationResult<User>.Successfull(userFromDB)));

            var query = new LoginUserQuery(loginUserDto);
            var handler = new LoginUserQueryHandler(mockRepository, tokenHelper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data); // Check that a token was returned
        }

    }
}

