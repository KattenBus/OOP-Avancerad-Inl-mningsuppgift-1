using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<AuthorDto>>
    {
        public Guid AuthorId { get; set; }

        public GetAuthorByIdQuery(Guid authorId) 
        {
            AuthorId = authorId;
        }
    }
}
