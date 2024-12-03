using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book?>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updatedBook = new Book
            {
                Id = request.BookId,
                Title = request.NewTitle,
                Description = request.NewDescription
            };

            return await _bookRepository.UpdateBook(request.BookId, updatedBook);
        }
    }
}
