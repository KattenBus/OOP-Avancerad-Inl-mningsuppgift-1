using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using Application.Dtos;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, OperationResult<List<BookDto>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task <OperationResult<List<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var allBooksFromDB = await _bookRepository.GetAllBooksList();

            if (allBooksFromDB.isSuccessfull)
            {
                var allBooksRepsonse = new List<BookDto>();

                foreach (var book in allBooksFromDB.Data)
                {
                    var bookDto = new BookDto()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                    };
                    allBooksRepsonse.Add(bookDto);
                }

                return OperationResult<List<BookDto>>.Successfull(allBooksRepsonse);
            }
            else
            {
                return OperationResult<List<BookDto>>.Failure(allBooksFromDB.ErrorMessage);
            }
        }
    }
}
