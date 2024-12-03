using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;
        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
                await _authorRepository.AddAuthor(request.AuthorToAdd);

                return request.AuthorToAdd;
        }
    }
}
