using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<OperationResult<AuthorDto>>
    {
        public Guid AuthorId { get; set; }
        public DeleteAuthorCommand(Guid authorId) 
        { 
            AuthorId = authorId;
        }
    }
}
