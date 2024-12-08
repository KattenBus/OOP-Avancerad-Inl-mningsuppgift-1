using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using Application.Dtos;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthorToAddInDB = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = request.AuthorToAdd.FirstName,
                LastName = request.AuthorToAdd.LastName
            };

            var result = await _authorRepository.AddAuthor(newAuthorToAddInDB);

            if (result.isSuccessfull)
            {
                var addedAuthorDtoResponse = new AuthorDto
                {
                    Id = request.AuthorToAdd.Id,
                    FirstName = request.AuthorToAdd.FirstName,
                    LastName = request.AuthorToAdd.LastName,
                };

                return OperationResult<AuthorDto>.Successfull(addedAuthorDtoResponse);
            }
            else
            {
                return OperationResult<AuthorDto>.Failure(result.ErrorMessage);
            }
        }
    }
}
