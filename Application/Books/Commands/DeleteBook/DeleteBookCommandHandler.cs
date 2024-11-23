using MediatR;
using Infrastructure.Database;
using Domain;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book?>
    {
        private readonly FakeDatabase _database;

        public DeleteBookCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<Book?> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToRemove = _database.Books.FirstOrDefault(book => book.Id == request.BookId);

            if (bookToRemove != null)
            {
                _database.Books.Remove(bookToRemove);
                return Task.FromResult<Book?>(bookToRemove);
            }

            return Task.FromResult<Book?>(null);
        }
    }
}
