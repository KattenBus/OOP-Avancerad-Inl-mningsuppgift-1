using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _database;

        public GetBookByIdQueryHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Book>Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var getBookByID = _database.Books.FirstOrDefault(book  => book.Id == request.BookId);
            if (getBookByID == null)
            {
                throw new ArgumentException($"Book with ID {request.BookId} not found.");
            }
            return Task.FromResult( getBookByID );
        }
    }
}
