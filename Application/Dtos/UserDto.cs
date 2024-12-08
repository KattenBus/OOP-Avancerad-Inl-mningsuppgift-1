
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public  required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters long.")]
        public required string Password { get; set; }

    }
}
