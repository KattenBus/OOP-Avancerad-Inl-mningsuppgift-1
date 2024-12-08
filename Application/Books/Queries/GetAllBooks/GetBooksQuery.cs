using MediatR;
using Domain;
using Application.Dtos;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetBooksQuery : IRequest<OperationResult<List<BookDto>>>
    {

    }
}
