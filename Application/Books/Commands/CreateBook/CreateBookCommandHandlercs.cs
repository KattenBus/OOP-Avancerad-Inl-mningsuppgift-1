
using MediatR;
using Domain;
using Infrastructure.Database;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Domain.Book>
    {
        private readonly FakeDatabase _database;

        public CreateBookCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Domain.Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            _database.Books.Add(request.BookToAdd);

            return Task.FromResult(request.BookToAdd);
        }
    }
}
