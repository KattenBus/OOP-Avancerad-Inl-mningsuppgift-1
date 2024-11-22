using MediatR;
using Infrastructure.Database;
using Domain;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Domain.Book>
    {
        private readonly FakeDatabase _database;

        public UpdateBookCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Domain.Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _database.Books.FirstOrDefault(book => book.Id == request.BookId);

            if (bookToUpdate == null)
            {
                throw new InvalidOperationException($"Book with ID {request.BookId} not found.");
            }

            bookToUpdate.Title = request.NewTitle;
            bookToUpdate.Description = request.NewDescription;

            return Task.FromResult(bookToUpdate);
        }
    }
}
