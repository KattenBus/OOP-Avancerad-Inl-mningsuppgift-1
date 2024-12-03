//using Application.Books.Commands.CreateBook;
//using Application.Books.Commands.DeleteBook;
//using Application.Books.Commands.UpdateBook;
//using Domain;
//using Infrastructure.Database;
//using Application.Books.Queries.GetAllBooks;
//using Application.Books.Queries.GetBookById;

//namespace Tests.BookTests
//{
//    [TestFixture]
//    public class BookHandlerTests
//    {
//        private FakeDatabase _fakeDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the fake database before each test
//            _fakeDatabase = new FakeDatabase();
//        }

//        [Test]
//        public async Task Handle_GetAllBooksQuery_ReturnsAllBooks()
//        {
//            // Arrange
//            var handler = new GetBooksQueryHandler(_fakeDatabase);
//            var query = new GetBooksQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Count, Is.EqualTo(_fakeDatabase.Books.Count));
//        }

//        [Test]
//        public async Task Handle_CreateBookCommand_AddsBookToDatabase()
//        {
//            // Arrange
//            var handler = new CreateBookCommandHandler(_fakeDatabase);
//            var newBook = new Book(6, "New Book", "New Description");
//            var command = new CreateBookCommand(newBook);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(_fakeDatabase.Books.Contains(result));
//            Assert.That(result.Title, Is.EqualTo("New Book"));
//            Assert.That(result.Description, Is.EqualTo("New Description"));
//        }
//        [Test]
//        public async Task Handle_UpdateBookCommand_UpdatesBookInDatabase()
//        {
//            // Arrange
//            var handler = new UpdateBookCommandHandler(_fakeDatabase);
//            var bookId = 1;
//            var newTitle = "Updated Title";
//            var newDescription = "Updated Description";
//            var command = new UpdateBookCommand(bookId, newTitle, newDescription);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(bookId));
//            Assert.That(result.Title, Is.EqualTo(newTitle));
//            Assert.That(result.Description, Is.EqualTo(newDescription));
//        }
//        [Test]
//        public async Task Handle_DeleteBookCommand_RemovesBookFromDatabase()
//        {
//            // Arrange
//            var handler = new DeleteBookCommandHandler(_fakeDatabase);
//            var bookIdToDelete = 2;
//            var command = new DeleteBookCommand(bookIdToDelete);

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(_fakeDatabase.Books.Find(book => book.Id == bookIdToDelete), Is.Null);
//        }
//        [Test]
//        public async Task Handle_GetBookByIdQuery_ReturnsCorrectBook()
//        {
//            // Arrange
//            var bookIdToRetrieve = 1; 
//            var handler = new GetBookByIdQueryHandler(_fakeDatabase);
//            var query = new GetBookByIdQuery(bookIdToRetrieve);

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Id, Is.EqualTo(bookIdToRetrieve));
//            Assert.That(result.Title, Is.EqualTo(_fakeDatabase.Books.First(book => book.Id == bookIdToRetrieve).Title));
//            Assert.That(result.Description, Is.EqualTo(_fakeDatabase.Books.First(book => book.Id == bookIdToRetrieve).Description));
//        }
//    }
//}
