
//using Application.Authors.Commands.CreateAuthor;
//using Application.Authors.Commands.DeleteAuthor;
//using Application.Authors.Commands.UpdateAuthor;
//using Application.Authors.Queries.GetAllAuthors;
//using Application.Authors.Queries.GetAuthorById;
//using Domain;
//using Infrastructure.Database;

//namespace Tests.BookTests
//{
//    [TestFixture]
//    public class AuthorHandlerTests
//    {
//        private FakeDatabase _fakeDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the fake database before each test
//            _fakeDatabase = new FakeDatabase();
//        }

//        [Test]
//        public async Task Handle_GetAllAuthorsQuery_ReturnsAllAuthors()
//        {
//            // Arrange
//            var handler = new GetAuthorsQueryHandler(_fakeDatabase);
//            var query = new GetAuthorsQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Count, Is.EqualTo(_fakeDatabase.Authors.Count));
//        }

//        [Test]
//        public async Task Handle_CreateAuthorCommand_AddsAuthorToDatabase()
//        {
//            // Arrange
//            var handler = new CreateAuthorCommandHandler(_fakeDatabase);
//            var newAuthor = new Author(6, "J.R", "Tolkien");
//            var command = new CreateAuthorCommand(newAuthor);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(_fakeDatabase.Authors.Contains(result));
//            Assert.That(result.FirstName, Is.EqualTo("J.R"));
//            Assert.That(result.LastName, Is.EqualTo("Tolkien"));
//        }
//        [Test]
//        public async Task Handle_UpdateAuthorCommand_UpdatesAuthorInDatabase()
//        {
//            // Arrange
//            var handler = new UpdateAuthorCommandHandler(_fakeDatabase);
//            var authorId = 1;
//            var newFirstName = "Updated FirstName";
//            var newLastName = "Updated LastName";
//            var command = new UpdateAuthorCommand(authorId, newFirstName, newLastName);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(authorId));
//            Assert.That(result.FirstName, Is.EqualTo(newFirstName));
//            Assert.That(result.LastName, Is.EqualTo(newLastName));
//        }
//        [Test]
//        public async Task Handle_DeleteAuthorCommand_RemovesAuthorFromDatabase()
//        {
//            // Arrange
//            var handler = new DeleteAuthorCommandHandler(_fakeDatabase);
//            var authorIdToDelete = 2;
//            var command = new DeleteAuthorCommand(authorIdToDelete);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(_fakeDatabase.Authors.Find(author => author.Id == authorIdToDelete), Is.Null);
//        }
//        [Test]
//        public async Task Handle_GetAuthorByIdQuery_ReturnsCorrectAuthor()
//        {
//            // Arrange
//            var authorIdToRetrieve = 1;
//            var handler = new GetAuthorByIdQueryHandler(_fakeDatabase);
//            var query = new GetAuthorByIdQuery(authorIdToRetrieve);

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(authorIdToRetrieve));
//            Assert.That(result.FirstName, Is.EqualTo(_fakeDatabase.Authors.First(author => author.Id == authorIdToRetrieve).FirstName));
//            Assert.That(result.LastName, Is.EqualTo(_fakeDatabase.Authors.First(author => author.Id == authorIdToRetrieve).LastName ));
//        }
//    }
//}
