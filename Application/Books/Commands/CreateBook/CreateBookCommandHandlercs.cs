using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
           await _bookRepository.AddBook(request.BookToAdd);

            return request.BookToAdd;
        }
    }
}
