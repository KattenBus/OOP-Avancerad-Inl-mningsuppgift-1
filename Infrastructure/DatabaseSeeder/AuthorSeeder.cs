using Application.Interfaces.RepositoryInterfaces;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AuthorSeeder
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorSeeder(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task SeedAsync()
    {
        var authorsInDB = await _authorRepository.GetAllAuthorList();

        var existingAuthors = authorsInDB.Data ?? new List<Author>();

        var testAuthors = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName1", LastName = "LastName1" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName2", LastName = "LastName2" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName3", LastName = "LastName3" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName4", LastName = "LastName4" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName5", LastName = "LastName5" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName6", LastName = "LastName6" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName7", LastName = "LastName7" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName8", LastName = "LastName8" },
            new Author { Id = Guid.NewGuid(), FirstName = "FirstName9", LastName = "LastName9" },
        };

        foreach (var testAuthor in testAuthors)
        {
            if (!existingAuthors.Any(author => author.FirstName == testAuthor.FirstName && author.LastName == testAuthor.LastName))
            {
                await _authorRepository.AddAuthor(testAuthor);
            }
        }
    }
}
