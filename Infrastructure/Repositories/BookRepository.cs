
using Application.Interfaces.RepositoryInterfaces;
using Azure.Core;
using Domain;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _realDatabase;

        public BookRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<OperationResult<List<Book>>> GetAllBooksList()
        {
            try
            {
                var allBooks = await _realDatabase.Books.ToListAsync();
                if (allBooks == null)
                {
                    return OperationResult<List<Book>>.Failure("No books found in the database");
                }
                else
                {
                    return OperationResult<List<Book>>.Successfull(allBooks);
                }
                    
            }
            catch (Exception ex)
            {
                return OperationResult<List<Book>>.Failure(errorMessage: $"An error occurred while fetching all Books from the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Book>> GetBookById(Guid id)
        {
            try
            {
                var getBookByID = await _realDatabase.Books.FirstOrDefaultAsync(book => book.Id == id);

                if (getBookByID == null)
                {
                    return OperationResult<Book>.Failure($"No book with {id} found in the database");
                }
                else
                {
                    return OperationResult<Book>.Successfull(getBookByID);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure(errorMessage: $"An error occurred while fetching Book with {id} from the Database: {ex.Message}");
            }
        }
        public async Task<OperationResult<Book>> AddBook(Book book)
        {
            try
            {
                _realDatabase.Books.Add(book);
                await _realDatabase.SaveChangesAsync();

                return OperationResult<Book>.Successfull(book);
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure(errorMessage: $"An error occurred while adding Book to the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Book>> UpdateBook(Guid id, Book book)
        {
            try
            {
                var bookToUpdate = _realDatabase.Books.FirstOrDefault(book => book.Id == id);

                if (bookToUpdate == null)
                {
                    return OperationResult<Book>.Failure($"No book with {id} found in the database");
                }
                else
                {
                    bookToUpdate.Title = book.Title;
                    bookToUpdate.Description = book.Description;

                    await _realDatabase.SaveChangesAsync();

                    return OperationResult<Book>.Successfull(bookToUpdate);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure(errorMessage: $"An error occurred while updating Book with {id} to the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Book>> DeleteBookById(Guid id)
        {
            try
            {
                var bookToDelete = _realDatabase.Books.FirstOrDefault(book => book.Id == id);

                if (bookToDelete == null)
                {
                    return OperationResult<Book>.Failure($"No book with {id} found in the database");
                }
                else
                {
                    _realDatabase.Books.Remove(bookToDelete);
                    await _realDatabase.SaveChangesAsync();

                    return OperationResult<Book>.Successfull(bookToDelete);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure(errorMessage: $"An error occurred while deleting Book with {id} to the Database: {ex.Message}");
            }
        }
    }
}
