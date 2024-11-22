using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Authors.Queries.GetAllAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _database;

        public GetAuthorsQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var allAuthors = _database.Authors;

            return Task.FromResult(allAuthors);
        }
    }
}
