using System.ComponentModel.DataAnnotations;

namespace Learning_ASP_Dot_Net.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}