using MediatR;
using Domain;
using Application.Dtos;

namespace Application.Authors.Queries.GetAllAuthors
{
    public class GetAuthorsQuery : IRequest<OperationResult<List<AuthorDto>>>
    {

    }
}
