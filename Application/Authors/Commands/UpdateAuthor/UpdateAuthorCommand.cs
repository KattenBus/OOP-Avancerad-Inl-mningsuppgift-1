using Domain;
using MediatR;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Author?>
    {
        public int AuthorId { get; }
        public string NewAuthorFirstName { get; }
        public string NewAuthorLastName { get; }

        public UpdateAuthorCommand(int authorId, string newAuthorFirstName, string newAuthorLastName)
        {
            AuthorId = authorId;
            NewAuthorFirstName = newAuthorFirstName;
            NewAuthorLastName = newAuthorLastName;
        }
    }
}
