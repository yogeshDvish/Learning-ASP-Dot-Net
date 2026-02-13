using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.DataAccess.Interfaces;
using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP_Dot_Net.DataAccess
{
    public class StudentControllDataAccess : IStudentControllDataAccess

    {
        private readonly ApplicationDbContext _context;

        public StudentControllDataAccess(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateStudents(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
