using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var allBooks = await _bookRepository.GetAllBooksList();

            return allBooks;
        }
    }
}
