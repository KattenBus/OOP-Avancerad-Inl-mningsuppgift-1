using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author?>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToRemove = await _authorRepository.DeleteAuthorById(request.AuthorId);

            return authorToRemove;
        }
    }
}
