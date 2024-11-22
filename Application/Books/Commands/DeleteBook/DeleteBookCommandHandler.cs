using MediatR;
using Infrastructure.Database;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly FakeDatabase _database;

        public DeleteBookCommandHandler(FakeDatabase database)
        {
            _database = database;
        }

        public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToRemove = _database.Books.FirstOrDefault(book => book.Id == request.BookId);

            if (bookToRemove != null)
            {
                _database.Books.Remove(bookToRemove);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
