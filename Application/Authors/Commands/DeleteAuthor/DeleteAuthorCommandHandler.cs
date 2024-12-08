using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToRemove = await _authorRepository.DeleteAuthorById(request.AuthorId);

            if (authorToRemove.isSuccessfull)
            {
                var authorResponse = new AuthorDto
                {
                    FirstName = authorToRemove.Data.FirstName,
                    LastName = authorToRemove.Data.LastName,
                };

                return OperationResult<AuthorDto>.Successfull(authorResponse);
            }
            else
            {
                return OperationResult<AuthorDto>.Failure(authorToRemove.ErrorMessage);
            }
        }
    }
}
