using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using Application.Dtos;

namespace Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = await _bookRepository.DeleteBookById(request.BookId);

            if (bookToDelete.isSuccessfull)
            {
                var bookResponse = new BookDto
                {
                    Title = bookToDelete.Data.Title,
                    Description = bookToDelete.Data.Description
                };
                return OperationResult<BookDto>.Successfull(bookResponse);
            }
            else
            {
                return OperationResult<BookDto>.Failure(bookToDelete.ErrorMessage);
            }
        }
    }
}
