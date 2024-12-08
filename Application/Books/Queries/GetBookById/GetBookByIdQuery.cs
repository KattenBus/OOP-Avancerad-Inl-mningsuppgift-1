using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Books.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<OperationResult<BookDto>>
    {
        public Guid BookId { get; set; }

        public GetBookByIdQuery(Guid bookId) 
        {
            BookId = bookId;
        }
    }
}
