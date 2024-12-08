using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using Application.Dtos;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updatedBook = new Book
            {
                Id = request.BookId,
                Title = request.NewTitle,
                Description = request.NewDescription
            };
            var updatedBookFromDB = await _bookRepository.UpdateBook(request.BookId, updatedBook);

            if (updatedBookFromDB.isSuccessfull)
            {
                var updatedBookResponse = new BookDto
                {
                    Id = updatedBookFromDB.Data.Id,
                    Title = updatedBookFromDB.Data.Title,
                    Description = updatedBookFromDB.Data.Description
                };

                return OperationResult<BookDto>.Successfull(updatedBookResponse);
            }
            else
            {
                return OperationResult<BookDto>.Failure(updatedBookFromDB.ErrorMessage);
            }
        }
    }
}
