using System.ComponentModel.DataAnnotations;

namespace Learning_ASP_Dot_Net.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

    }
}
