using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<OperationResult<BookDto>>
    {
        public Guid BookId { get; }

        public DeleteBookCommand(Guid bookId) 
        {
            BookId = bookId;
        }
    }
}
