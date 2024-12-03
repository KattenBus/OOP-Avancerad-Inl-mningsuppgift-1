using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author?>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var getAuthorByID =  await _authorRepository.GetAuthorById(request.AuthorId);

            return getAuthorByID;
        }
    }
}
