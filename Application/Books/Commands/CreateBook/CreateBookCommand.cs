
using Domain;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand: IRequest<Domain.Book>
    {
        public CreateBookCommand(Domain.Book bookToAdd) 
        {
            BookToAdd = bookToAdd;
        }
        public Domain.Book BookToAdd { get; set; }
    }
}
