using Application.Interfaces.RepositoryInterfaces;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _realDatabase;

        public AuthorRepository (RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<OperationResult<List<Author>>> GetAllAuthorList()
        {
            try
            {
                var allAuthors = await _realDatabase.Authors.ToListAsync();

                if (allAuthors == null)
                {
                    return OperationResult<List<Author>>.Failure("No authors found in the database.");
                }
                else
                {
                    return OperationResult<List<Author>>.Successfull(allAuthors);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<Author>>.Failure(errorMessage: $"An error occurred while fetching all Authors from the Database: {ex.Message}");
            }

        }

        public async Task<OperationResult<Author?>> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _realDatabase.Authors.FirstOrDefaultAsync(author => author.Id == id);

                if (author == null)
                {
                    return OperationResult<Author?>.Failure($"No Author with ID {id} found in the Database!");
                }
                else
                {
                    return OperationResult<Author?>.Successfull(author);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Author?>.Failure(errorMessage: $"An error occurred while fetching the author from the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> AddAuthor(Author author)
        {
            try
            {
                _realDatabase.Authors.Add(author);
                await _realDatabase.SaveChangesAsync();

                return OperationResult<Author>.Successfull(author);
            }
            catch(Exception ex)
            {
                return OperationResult<Author>.Failure(errorMessage: $"An error occurred while adding the author to the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> UpdateAuthor(Guid id, Author author)
        {
            try
            {
                var authorToUpdate = _realDatabase.Authors.FirstOrDefault(author => author.Id == id);

                if (authorToUpdate == null)
                {
                    return OperationResult<Author>.Failure($"No Author with ID {id} found in the Database!");
                }
                else
                {
                    authorToUpdate.FirstName = author.FirstName;
                    authorToUpdate.LastName = author.LastName;

                    await _realDatabase.SaveChangesAsync();

                    return OperationResult<Author>.Successfull(authorToUpdate);
                }
            }
            catch( Exception ex)
            {
                return OperationResult<Author>.Failure(errorMessage: $"An error occurred while updating the author to the Database: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> DeleteAuthorById(Guid id)
        {
            try
            {
                var authorToDelete = _realDatabase.Authors.FirstOrDefault(author => author.Id == id);

                if (authorToDelete == null)
                {
                    return OperationResult<Author>.Failure($"No Author with {id} found in the Database!");
                }
                else
                {
                    _realDatabase.Authors.Remove(authorToDelete);
                    await _realDatabase.SaveChangesAsync();

                    return OperationResult<Author>.Successfull(authorToDelete);
                }
            }
            catch ( Exception ex )
            {
                return OperationResult<Author>.Failure(errorMessage: $"An error occurred while deleting the author to the Database: {ex.Message}");
            }
        }
    }
}
