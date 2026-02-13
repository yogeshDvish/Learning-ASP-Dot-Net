using Learning_ASP_Dot_Net.Behaviours.Interfaces;
using Learning_ASP_Dot_Net.DataAccess.Interfaces;
using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP_Dot_Net.Behaviours
{
    public class StudentControllBehaviour : IStudentControllBehaviour
    {
        IStudentControllDataAccess _dataAccess;
        public StudentControllBehaviour(IStudentControllDataAccess dataAccess) {
            _dataAccess = dataAccess;
        }
        public async Task<Student> CreateStudents(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Age = dto.Age,
                Email = dto.Email
            };

            Task<Student> studs = _dataAccess.CreateStudents(student);
            return student;
        }
    }
}
