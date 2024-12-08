using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using Application.Dtos;
using System.Net.NetworkInformation;

namespace Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        { 
            var newBookToAddInDB = new Book
            {
                Id = Guid.NewGuid(),
                Title = request.BookToAdd.Title,
                Description = request.BookToAdd.Description
            };

            var result = await _bookRepository.AddBook(newBookToAddInDB);

            if (result.isSuccessfull)
            {
                var bookToAddResponse = new BookDto
                {
                    Id = request.BookToAdd.Id,
                    Title = request.BookToAdd.Title,
                    Description = request.BookToAdd.Description
                };

                return OperationResult<BookDto>.Successfull(bookToAddResponse);
            }
            else
            {
                return OperationResult<BookDto>.Failure(result.ErrorMessage);
            }
        }
    }
}
