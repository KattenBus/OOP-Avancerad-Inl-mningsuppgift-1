using Domain;
using MediatR;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Book?>
    {
        public int BookId { get; }

        public DeleteBookCommand(int bookId) 
        {
            BookId = bookId;
        }
    }
}
