using Application.Interfaces.RepositoryInterfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BookSeeder
{
    private readonly IBookRepository _bookRepository;

    public BookSeeder(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task SeedAsync()
    {
        var booksInDB = await _bookRepository.GetAllBooksList();

        var existingBooks = booksInDB.Data ?? new List<Book>();

        var testBooks = new List<Book>
        {
            new Book { Id = Guid.NewGuid(), Title = "Book 1", Description = "Description1" },
            new Book { Id = Guid.NewGuid(), Title = "Book 2", Description = "Description2" },
            new Book { Id = Guid.NewGuid(), Title = "Book 3", Description = "Description3" },
            new Book { Id = Guid.NewGuid(), Title = "Book 4", Description = "Description4" },
            new Book { Id = Guid.NewGuid(), Title = "Book 5", Description = "Description5" },
            new Book { Id = Guid.NewGuid(), Title = "Book 6", Description = "Description6" },
            new Book { Id = Guid.NewGuid(), Title = "Book 7", Description = "Description7" },
            new Book { Id = Guid.NewGuid(), Title = "Book 8", Description = "Description8" },
            new Book { Id = Guid.NewGuid(), Title = "Book 9", Description = "Description9" },
        };

        foreach (var testBook in testBooks)
        {
            if (!existingBooks.Any(book => book.Title == testBook.Title))
            {
                await _bookRepository.AddBook(testBook);
            }
        }
    }
}
