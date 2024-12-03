using Domain;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<Book?>
    {
        public int BookId { get; set; }

        public GetBookByIdQuery(int bookId) 
        {
            BookId = bookId;
        }
    }
}
