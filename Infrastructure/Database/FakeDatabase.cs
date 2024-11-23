using Domain;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List<Book> Books 
        { 
            get { return AllBooksFromDB; }
            set { AllBooksFromDB = value; } 
        }

        private static List<Book> AllBooksFromDB = new List<Book>
        {
            new Book (1, "newBook1", "Beskrivning1"),
            new Book (2, "newBook2", "Beskrivning2"),
            new Book (3, "newBook3", "Beskrivning3"),
            new Book (4, "newBook4", "Beskrivning4"),
            new Book (5, "newBook5", "Beskrivning5")
        };

        public List<Author> Authors
        {
            get { return AllAuthorsFromDB; }
            set { AllAuthorsFromDB = value; }
        }

        private static List<Author> AllAuthorsFromDB = new List<Author>
        {
            new Author (1, "FirstName1", "LastName1"),
            new Author (2, "FirstName2", "LastName2"),
            new Author (3, "FirstName3", "LastName3"),
            new Author (4, "FirstName4", "LastName4"),
            new Author (5, "FirstName5", "LastName5")
        };

        public List<User> Users
        {
            get { return AllUsersFromDB; }
            set { AllUsersFromDB = value; }
        }

        private static List<User> AllUsersFromDB = new List<User>
        {
            new User {Id = Guid.NewGuid(), UserName = "Simon"},
            new User {Id = Guid.NewGuid(), UserName = "Öman"},
            new User {Id = Guid.NewGuid(), UserName = "Rinne"},
            new User {Id = Guid.NewGuid(), UserName = "Mikael"},
            new User {Id = Guid.NewGuid(), UserName = "Per"},
        };
    }
}
