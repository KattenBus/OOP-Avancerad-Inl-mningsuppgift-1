using Domain;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(int authorId) 
        { 
            AuthorId = authorId;
        }
    }
}
