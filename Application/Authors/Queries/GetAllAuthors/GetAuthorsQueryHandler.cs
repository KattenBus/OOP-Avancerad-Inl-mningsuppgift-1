using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Castle.Core.Logging;
using Domain;
using MediatR;

namespace Application.Authors.Queries.GetAllAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, OperationResult<List<AuthorDto>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task <OperationResult<List<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var allAuthorsFromDB = await _authorRepository.GetAllAuthorList();

            if (allAuthorsFromDB.isSuccessfull)
            {
                var allAuthorsResponse = new List<AuthorDto>();

                foreach (var author in allAuthorsFromDB.Data)
                {
                    var authorDto = new AuthorDto
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                    };
                    allAuthorsResponse.Add(authorDto);
                }

                return OperationResult<List<AuthorDto>>.Successfull(allAuthorsResponse);
            }
            else
            {
                return OperationResult<List<AuthorDto>>.Failure(allAuthorsFromDB.ErrorMessage);
            }

        }
    }
}
