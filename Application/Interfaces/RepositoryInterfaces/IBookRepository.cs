using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        //Metoder
        Task <OperationResult<List<Book>>> GetAllBooksList();

        Task<OperationResult<Book>> GetBookById(Guid id);

        Task<OperationResult<Book>> AddBook(Book book);

        Task<OperationResult<Book>> UpdateBook(Guid id, Book book);

        Task<OperationResult<Book>> DeleteBookById(Guid id);
    }
}
