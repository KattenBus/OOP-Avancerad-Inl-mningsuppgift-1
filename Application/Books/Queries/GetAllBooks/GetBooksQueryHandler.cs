
using MediatR;
using Infrastructure.Database;
using Domain;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly FakeDatabase _database;

        public GetBooksQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var allBooks = _database.Books;
            return Task.FromResult(allBooks);
        }
    }
}
