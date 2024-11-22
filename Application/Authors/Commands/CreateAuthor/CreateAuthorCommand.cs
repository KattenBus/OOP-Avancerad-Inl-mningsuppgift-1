using MediatR;
using Domain;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Author>
    {
        public CreateAuthorCommand(Author authorToAdd)
        {
            AuthorToAdd = authorToAdd;
        }
        public Author AuthorToAdd { get; set; }
    }
}
