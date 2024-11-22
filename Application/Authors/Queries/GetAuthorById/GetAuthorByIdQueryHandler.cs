using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _database;

        public GetAuthorByIdQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Author>Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var getAuthorByID = _database.Authors.FirstOrDefault(author  => author.Id == request.AuthorId);
            if (getAuthorByID == null)
            {
                throw new ArgumentException($"Author with ID {request.AuthorId} not found.");
            }
            return Task.FromResult( getAuthorByID );
        }
    }
}
