using MediatR;
using Domain;
using Infrastructure.Database;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly FakeDatabase _database;
        public CreateAuthorCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            _database.Authors.Add(request.AuthorToAdd);

            return Task.FromResult(request.AuthorToAdd);
        }
    }
}
