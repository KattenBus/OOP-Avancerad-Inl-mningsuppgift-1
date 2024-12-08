using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBooks;
using Application.Books.Queries.GetBookById;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using FakeItEasy;

namespace Tests.IntegrationTests
{
    public class BookIntegrationsTests
    {
        [Test]
        public async Task GetBooksQueryHandler_ReturnsListOfBookDtos_WhenBooksExists()
        {
            // Arrange
            var mockRepository = A.Fake<IBookRepository>();

            var booksList = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Title1", Description = "Description1" },
                new Book { Id = Guid.NewGuid(), Title = "Title2", Description = "Description2" }
            };

            A.CallTo(() => mockRepository.GetAllBooksList())
                .Returns(Task.FromResult(OperationResult<List<Book>>.Successfull(booksList)));

            var query = new GetBooksQuery();
            var handler = new GetBooksQueryHandler(mockRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data);
            Assert.That(booksList.Count, Is.EqualTo(result.Data.Count));

            for (int index = 0; index < booksList.Count; index++)
            {
                Assert.That(booksList[index].Id, Is.EqualTo(result.Data[index].Id));
                Assert.That(booksList[index].Title, Is.EqualTo(result.Data[index].Title));
                Assert.That(booksList[index].Description, Is.EqualTo(result.Data[index].Description));
            }
        }

        [Test]
        public async Task GetBookByIdHandler_ReturnsBookDto_WhenBookExists()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var mockRepository = A.Fake<IBookRepository>();
            var testBook = new Book { Id = bookId, Title = "Lord Of The Rings", Description = "Description of Lord OF The Rings" };

            A.CallTo(() => mockRepository.GetBookById(bookId))
                .Returns(Task.FromResult(OperationResult<Book>.Successfull(testBook)));

            var query = new GetBookByIdQuery(bookId);
            var handler = new GetBookByIdQueryHandler(mockRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.IsNotNull(result.Data);
            Assert.That(testBook.Id, Is.EqualTo(result.Data.Id));
            Assert.That(testBook.Title, Is.EqualTo(result.Data.Title));
            Assert.That(testBook.Description, Is.EqualTo(result.Data.Description));
        }

        [Test]
        public async Task CreateBookCommandHandler_ReturnsBookDto_WhenBookIsCreated()
        {
            // Arrange
            var newBookDto = new BookDto { Title = "Harry Potter", Description = "Description of Harry Potter" };
            var mockRepository = A.Fake<IBookRepository>();
            var newBookToAdd = new Book
            {
                Id = Guid.NewGuid(),
                Title = newBookDto.Title,
                Description = newBookDto.Description
            };

            A.CallTo(() => mockRepository.AddBook(A<Book>.That.Matches(book =>
                book.Title == newBookDto.Title && book.Description == newBookDto.Description)))
                .Returns(Task.FromResult(OperationResult<Book>.Successfull(newBookToAdd)));

            var command = new CreateBookCommand(newBookDto);
            var handler = new CreateBookCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(newBookDto.Title, Is.EqualTo(result.Data.Title));
            Assert.That(newBookDto.Description, Is.EqualTo(result.Data.Description));
        }


        [Test]
        public async Task UpdateBookCommandHandler_ReturnsUpdatedBookDto_WhenBookIsUpdated()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var updatedBookDto = new BookDto
            {
                Id = bookId,
                Title = "The Lion, The Witch, and The Wardrobe",
                Description = "Description of The Lion, The Witch, and The Wardrobe"
            };
            var mockRepository = A.Fake<IBookRepository>();
            var updatedBookFromDB = new Book
            {
                Id = bookId,
                Title = updatedBookDto.Title,
                Description = updatedBookDto.Description
            };

            A.CallTo(() => mockRepository.UpdateBook(bookId, A<Book>.That.Matches(book =>
                book.Title == updatedBookDto.Title && book.Description == updatedBookDto.Description)))
                .Returns(Task.FromResult(OperationResult<Book>.Successfull(updatedBookFromDB)));

            var command = new UpdateBookCommand(bookId, updatedBookDto.Title, updatedBookDto.Description);
            var handler = new UpdateBookCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(updatedBookDto.Title, Is.EqualTo(result.Data.Title));
            Assert.That(updatedBookDto.Description, Is.EqualTo(result.Data.Description));
        }


        [Test]
        public async Task DeleteBookCommandHandler_ReturnsBookDto_WhenBookIsDeleted()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var mockRepository = A.Fake<IBookRepository>();
            var bookToDelete = new Book
            {
                Id = bookId,
                Title = "Around The World In 80 Days",
                Description = "Description of Around The World In 80 Days"
            };

            A.CallTo(() => mockRepository.DeleteBookById(bookId))
                .Returns(Task.FromResult(OperationResult<Book>.Successfull(bookToDelete)));

            var command = new DeleteBookCommand(bookId);
            var handler = new DeleteBookCommandHandler(mockRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.isSuccessfull);
            Assert.That(bookToDelete.Title, Is.EqualTo(result.Data.Title));
            Assert.That(bookToDelete.Description, Is.EqualTo(result.Data.Description));
        }
    }
}
