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

        public async Task<List<Author>> GetAllAuthorList()
        {
            return await _realDatabase.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            return await _realDatabase.Authors.FirstOrDefaultAsync(author => author.Id == id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
                _realDatabase.Authors.Add(author);
               await _realDatabase.SaveChangesAsync();

                return author;
        }

        public async Task<Author?> UpdateAuthor(int id, Author author)
        {
            var authorToUpdate = _realDatabase.Authors.FirstOrDefault(author => author.Id == id);

            if (authorToUpdate == null)
            {
                return null;
            }
            else
            {
                authorToUpdate.FirstName = author.FirstName;
                authorToUpdate.LastName = author.LastName;

                await _realDatabase.SaveChangesAsync();

                return authorToUpdate;
            }
        }

        public async Task<Author?> DeleteAuthorById(int id)
        {
            var authorToDelete = _realDatabase.Authors.FirstOrDefault(author => author.Id == id);

            if (authorToDelete == null) 
            {
                return null;
            }
            else
            {
                _realDatabase.Authors.Remove(authorToDelete);
                await _realDatabase.SaveChangesAsync();

                return authorToDelete;
            }
        }
    }
}
