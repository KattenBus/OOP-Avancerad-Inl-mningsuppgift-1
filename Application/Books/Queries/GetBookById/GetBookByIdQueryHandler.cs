using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var getBookByID = await _bookRepository.GetBookById(request.BookId);

            if (getBookByID.isSuccessfull)
            {
                var getBooksResponse = new BookDto
                {
                    Id = getBookByID.Data.Id,
                    Title = getBookByID.Data.Title,
                    Description = getBookByID.Data.Description,
                };

                return OperationResult<BookDto>.Successfull(getBooksResponse);
            }
            else
            {
                return OperationResult<BookDto>.Failure(getBookByID.ErrorMessage);
            }
        }
    }
}
