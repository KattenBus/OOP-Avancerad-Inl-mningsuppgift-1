using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        //Metoder
        Task<OperationResult<List<Author>>> GetAllAuthorList();

        Task<OperationResult<Author>> GetAuthorById(Guid id);

        Task<OperationResult<Author>> AddAuthor(Author author);

        Task<OperationResult<Author>> UpdateAuthor(Guid id, Author author);

        Task <OperationResult<Author>> DeleteAuthorById(Guid id);
    }
}
