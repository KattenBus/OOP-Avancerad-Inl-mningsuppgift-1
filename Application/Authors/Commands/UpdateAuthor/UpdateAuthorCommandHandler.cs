using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _database;
        public UpdateAuthorCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = _database.Authors.FirstOrDefault(author => author.Id == request.AuthorId);

            if (authorToUpdate == null)
            {
                throw new ArgumentException($"Author with ID {request.AuthorId} not found.");
            }

            authorToUpdate.FirstName = request.NewAuthorFirstName;
            authorToUpdate.LastName = request.NewAuthorLastName;

            return Task.FromResult( authorToUpdate );
        }
    }
}
