using MediatR;
using Domain;

namespace Application.Authors.Queries.GetAllAuthors
{
    public class GetAuthorsQuery : IRequest<List<Author>>
    {

    }
}
