using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;

namespace Learning_ASP_Dot_Net.Behaviours.Interfaces
{
    public interface IStudentControllBehaviour
    {
        public Task<Student> CreateStudents(CreateStudentDto students); 
    }
}
