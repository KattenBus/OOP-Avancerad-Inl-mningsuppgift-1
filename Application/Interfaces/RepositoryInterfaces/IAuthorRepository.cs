using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        //Metoder
        Task<List<Author>> GetAllAuthorList();

        Task<Author?> GetAuthorById(int id);

        Task<Author> AddAuthor(Author author);

        Task<Author?> UpdateAuthor(int id, Author author);

        Task <Author?> DeleteAuthorById(int id);
    }
}
