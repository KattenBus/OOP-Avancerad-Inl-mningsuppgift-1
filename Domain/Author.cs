
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Author(Guid id, string firstName, string lastName) 
        { 
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public Author() { }
    }
}
