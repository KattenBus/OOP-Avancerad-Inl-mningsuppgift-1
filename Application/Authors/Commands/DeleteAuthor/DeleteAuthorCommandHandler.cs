using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author?>
    {
        private readonly FakeDatabase _database;

        public DeleteAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _database = fakeDatabase;
        }

        public Task<Author?> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToRemove = _database.Authors.FirstOrDefault(author => author.Id == request.AuthorId);

            if (authorToRemove != null)
            {
                _database.Authors.Remove(authorToRemove);
                return Task.FromResult<Author?>(authorToRemove);
            }

            return Task.FromResult<Author?>(null);
        }
    }
}
