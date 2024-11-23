

using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Users.Commands
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, User>
    {
        private readonly FakeDatabase _database;

        public AddNewUserCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<User> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
            };

            _database.Users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }
}
