using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllAuthors;
using Application.Authors.Queries.GetAuthorById;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using FakeItEasy;

namespace Tests.IntegrationTests
{
    public class AuthorIntegrationTests
    {
        [Test]
        public async Task GetAuthorsQueryHandler_ReturnsListOfAuthorDtos_WhenAuthorsExists()
        {
            // Arrange
            var mockRepository = A.Fake<IAuthorRepository>();

            var authorsList = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), FirstName = "Author1", LastName = "LastName1" },
                new Author { Id = Guid.NewGuid(), FirstName = "Author2", LastName = "LastName2" }
            };

            A.CallTo(() => mockRepository.GetAllAuthorList())
                .Returns(Task.FromResult(OperationResult<List<Author>>.Successfull(authorsList)));

            var query = new GetAuthorsQuery();
            var handler = new GetAuthorsQueryHandler(mockRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data);
            Assert.That(authorsList.Count, Is.EqualTo(result.Data.Count));

            for (int index = 0; index < authorsList.Count; index++)
            {
                Assert.That(authorsList[index].Id, Is.EqualTo(result.Data[index].Id));
                Assert.That(authorsList[index].FirstName, Is.EqualTo(result.Data[index].FirstName));
                Assert.That(authorsList[index].LastName, Is.EqualTo(result.Data[index].LastName));
            }
        }

        [Test]
        public async Task GetAuthorByIdHandler_ReturnsAuthorDto_WhenAuthorExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var mockRepository = A.Fake<IAuthorRepository>();
            var testAuthor = new Author { Id = authorId, FirstName = "J.R", LastName = "Tolkien" };

            A.CallTo(() => mockRepository.GetAuthorById(authorId))
                .Returns(Task.FromResult(OperationResult<Author>.Successfull(testAuthor)));

            var query = new GetAuthorByIdQuery(authorId);
            var handler = new GetAuthorByIdQueryHandler(mockRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data);
            Assert.That(testAuthor.Id, Is.EqualTo(result.Data.Id));
            Assert.That(testAuthor.FirstName, Is.EqualTo(result.Data.FirstName));
            Assert.That(testAuthor.LastName, Is.EqualTo(result.Data.LastName));
        }

        [Test]
        public async Task CreateAuthorCommandHandler_ReturnsAuthorDto_WhenAuthorIsCreated()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var newAuthorDto = new AuthorDto { Id = authorId, FirstName = "George", LastName = "Orwell" };
            var mockRepository = A.Fake<IAuthorRepository>();

            var newAuthorToAdd = new Author
            {
                Id = newAuthorDto.Id,
                FirstName = newAuthorDto.FirstName,
                LastName = newAuthorDto.LastName
            };

            A.CallTo(() => mockRepository.AddAuthor(A<Author>.That.Matches(author =>
                author.FirstName == newAuthorDto.FirstName && author.LastName == newAuthorDto.LastName)))
                .Returns(Task.FromResult(OperationResult<Author>.Successfull(newAuthorToAdd)));

            var command = new CreateAuthorCommand(newAuthorDto);
            var handler = new CreateAuthorCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(newAuthorDto.FirstName, Is.EqualTo(result.Data.FirstName));
            Assert.That(newAuthorDto.LastName, Is.EqualTo(result.Data.LastName));
        }


        [Test]
        public async Task UpdateAuthorCommandHandler_ReturnsUpdatedAuthorDto_WhenAuthorIsUpdated()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var updatedAuthor = new AuthorDto { Id = authorId, FirstName = "Aldous", LastName = "Huxley" };
            var mockRepository = A.Fake<IAuthorRepository>();
            var updatedAuthorFromDB = new Author
            {
                Id = authorId,
                FirstName = updatedAuthor.FirstName,
                LastName = updatedAuthor.LastName
            };

            A.CallTo(() => mockRepository.UpdateAuthor(authorId, A<Author>.That.Matches(author =>
                author.FirstName == updatedAuthor.FirstName && author.LastName == updatedAuthor.LastName)))
                .Returns(Task.FromResult(OperationResult<Author>.Successfull(updatedAuthorFromDB)));

            var command = new UpdateAuthorCommand(authorId, updatedAuthor.FirstName, updatedAuthor.LastName);
            var handler = new UpdateAuthorCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(updatedAuthor.FirstName, Is.EqualTo(result.Data.FirstName));
            Assert.That(updatedAuthor.LastName, Is.EqualTo(result.Data.LastName));
        }


        [Test]
        public async Task DeleteAuthorCommandHandler_ReturnsAuthorDto_WhenAuthorIsDeleted()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var mockRepository = A.Fake<IAuthorRepository>();
            var authorToDelete = new Author
            {
                Id = authorId,
                FirstName = "Jane",
                LastName = "Austen"
            };

            A.CallTo(() => mockRepository.DeleteAuthorById(authorId))
                .Returns(Task.FromResult(OperationResult<Author>.Successfull(authorToDelete)));

            var command = new DeleteAuthorCommand(authorId);
            var handler = new DeleteAuthorCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(authorToDelete.FirstName, Is.EqualTo(result.Data.FirstName));
            Assert.That(authorToDelete.LastName, Is.EqualTo(result.Data.LastName));
        }
    }
}
