using MediatR;
using Domain;

namespace Application.Books.Queries.GetAllBooks
{
    public class GetBooksQuery : IRequest<List<Book>>
    {

    }
}
