using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<OperationResult<AuthorDto>>
    {
        public Guid AuthorId { get; }
        public string NewAuthorFirstName { get; }
        public string NewAuthorLastName { get; }

        public UpdateAuthorCommand(Guid authorId, string newAuthorFirstName, string newAuthorLastName)
        {
            AuthorId = authorId;
            NewAuthorFirstName = newAuthorFirstName;
            NewAuthorLastName = newAuthorLastName;
        }
    }
}
