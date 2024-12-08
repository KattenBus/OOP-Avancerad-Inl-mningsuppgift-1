using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;
using System.Diagnostics;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var getAuthorByID = await _authorRepository.GetAuthorById(request.AuthorId);
    
            if (getAuthorByID.isSuccessfull)
            {
                var getAuthorDtoResponse = new AuthorDto
                {
                    Id = getAuthorByID.Data.Id,
                    FirstName = getAuthorByID.Data.FirstName,
                    LastName = getAuthorByID.Data.LastName
                };

                return OperationResult<AuthorDto>.Successfull(getAuthorDtoResponse);
            }
            else
            {
                return OperationResult<AuthorDto>.Failure(getAuthorByID.ErrorMessage);
            }
        }
    }
}
