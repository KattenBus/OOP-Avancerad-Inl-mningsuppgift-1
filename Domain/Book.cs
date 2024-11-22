
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public Book( int id, string title, string description) 
        { 
            Id = id;
            Title = title;
            Description = description;
        }
        public Book() { }
    }
}
