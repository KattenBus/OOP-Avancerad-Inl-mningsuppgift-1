using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        //Metoder
        Task<List<Book>> GetAllBooksList();

        Task<Book?> GetBookById(int id);

        Task<Book> AddBook(Book book);

        Task<Book?> UpdateBook(int id, Book book);

        Task<Book?> DeleteBookById(int id);
    }
}
