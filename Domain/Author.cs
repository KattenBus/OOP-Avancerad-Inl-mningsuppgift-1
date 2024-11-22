
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(50, ErrorMessage = "Firstname cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(50, ErrorMessage = "Lastname cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        public Author(int id, string firstName, string lastName) 
        { 
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public Author() { }
    }
}
