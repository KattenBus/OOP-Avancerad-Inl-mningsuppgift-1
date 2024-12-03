using Domain;
using MediatR;

namespace Application.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<Author?>
    {
        public int AuthorId { get; set; }

        public GetAuthorByIdQuery(int authorId) 
        {
            AuthorId = authorId;
        }
    }
}
