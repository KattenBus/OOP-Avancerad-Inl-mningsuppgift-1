using MediatR;
using Application.Dtos;
using Domain;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<OperationResult<AuthorDto>>
    {
        public CreateAuthorCommand(AuthorDto authorToAdd)
        {
            AuthorToAdd = authorToAdd;
        }
        public AuthorDto AuthorToAdd { get; set; }
    }
}
