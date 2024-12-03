using Domain;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand: IRequest<Book>
    {
        public CreateBookCommand(Book bookToAdd) 
        {
            BookToAdd = bookToAdd;
        }
        public Book BookToAdd { get; set; }
    }
}
