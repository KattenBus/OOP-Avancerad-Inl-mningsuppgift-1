
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

        public async Task<List<Book>> GetAllBooksList()
        {
            return await _realDatabase.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _realDatabase.Books.FirstOrDefaultAsync(book => book.Id == id);
        }
        public async Task<Book> AddBook(Book book)
        {
            _realDatabase.Books.Add(book);
            await _realDatabase.SaveChangesAsync();

            return book;
        }

        public async Task<Book?> UpdateBook(int id, Book book)
        {
            var bookToUpdate = _realDatabase.Books.FirstOrDefault(book => book.Id == id);

            if (bookToUpdate == null)
            {
                throw new InvalidOperationException($"Book with ID {id} not found.");
            }

            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;

            await _realDatabase.SaveChangesAsync();

            return bookToUpdate;
        }

        public async Task<Book?> DeleteBookById(int id)
        {
            var bookToDelete = _realDatabase.Books.FirstOrDefault(book => book.Id == id);

            if (bookToDelete == null)
            {
                return null;
            }
            else
            {
                _realDatabase.Books.Remove(bookToDelete);
                await _realDatabase.SaveChangesAsync();

                return bookToDelete;
            }
        }
    }
}
