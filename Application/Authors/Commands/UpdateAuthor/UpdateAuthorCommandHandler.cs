using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _repository;
        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _repository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var updatedAuthor = new Author
            {
                Id = request.AuthorId,
                FirstName = request.NewAuthorFirstName,
                LastName = request.NewAuthorLastName
            };
            var updatedAuthorFromDB = await _repository.UpdateAuthor(request.AuthorId, updatedAuthor);

            if (updatedAuthorFromDB.isSuccessfull)
            {
                var updatedAuthorResponse = new AuthorDto
                {
                    Id = updatedAuthorFromDB.Data.Id,
                    FirstName = updatedAuthorFromDB.Data.FirstName,
                    LastName = updatedAuthorFromDB.Data.LastName
                };

                return OperationResult<AuthorDto>.Successfull(updatedAuthorResponse);
            }
            else
            {
                return OperationResult<AuthorDto>.Failure(updatedAuthorFromDB.ErrorMessage);
            }
        }
    }
}
