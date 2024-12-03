using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author?>
    {
        private readonly IAuthorRepository _repository;
        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _repository = authorRepository;
        }

        public async Task<Author?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var updatedAuthor = new Author
            {
                Id = request.AuthorId,
                FirstName = request.NewAuthorFirstName,
                LastName = request.NewAuthorLastName
            };

            return await _repository.UpdateAuthor(request.AuthorId, updatedAuthor);
        }
    }
}
