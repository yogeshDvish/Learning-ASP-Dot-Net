using System.ComponentModel.DataAnnotations;

namespace Learning_ASP_Dot_Net.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
