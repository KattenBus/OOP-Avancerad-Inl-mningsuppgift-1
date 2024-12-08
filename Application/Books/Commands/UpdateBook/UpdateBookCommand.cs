using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<OperationResult<BookDto>>
    {
        public Guid BookId { get; }
        public string NewTitle { get; }
        public string NewDescription { get; }

        public UpdateBookCommand(Guid bookId, string newTitle, string newDescription)
        {
            BookId = bookId;
            NewTitle = newTitle;
            NewDescription = newDescription;
        }
    }
}
