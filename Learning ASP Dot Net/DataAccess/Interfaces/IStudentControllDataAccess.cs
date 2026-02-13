using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;

namespace Learning_ASP_Dot_Net.DataAccess.Interfaces
{
    public interface IStudentControllDataAccess
    {
        public Task<Student> CreateStudents(Student student);
    }
}
