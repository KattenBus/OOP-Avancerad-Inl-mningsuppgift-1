using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommand: IRequest<OperationResult<BookDto>>
    {
        public CreateBookCommand(BookDto bookToAdd) 
        {
            BookToAdd = bookToAdd;
        }
        public BookDto BookToAdd { get; set; }
    }
}
