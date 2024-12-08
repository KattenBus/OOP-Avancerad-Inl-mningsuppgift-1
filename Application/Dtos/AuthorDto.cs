using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(20, ErrorMessage = "Firstname cannot exceed 20 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(20, ErrorMessage = "Lastname cannot exceed 20 characters.")]
        public string? LastName { get; set; }
    }
}
