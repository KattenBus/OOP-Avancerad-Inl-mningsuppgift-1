using MediatR;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Domain.Book>
    {
        public int BookId { get; }
        public string NewTitle { get; }
        public string NewDescription { get; }

        public UpdateBookCommand(int bookId, string newTitle, string newDescription)
        {
            BookId = bookId;
            NewTitle = newTitle;
            NewDescription = newDescription;
        }
    }
}
