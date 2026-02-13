using Learning_ASP_Dot_Net.Behaviours.Interfaces;
using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private IStudentControllBehaviour _studentControllBehaviour;

        public StudentsController(ApplicationDbContext context, IStudentControllBehaviour stud)
        {
            _context = context;
            _studentControllBehaviour = stud;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return student;
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(CreateStudentDto dto)
        {
            //var student = new Student
            //{
            //    Name = dto.Name,
            //    Age = dto.Age,
            //    Email = dto.Email
            //};

            //_context.Students.Add(student);
            //await _context.SaveChangesAsync();

            var student = _studentControllBehaviour.CreateStudents(dto);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            student.Name = dto.Name ?? student.Name;
            student.Age = dto.Age != 0 ? dto.Age : student.Age;
            student.Email = dto.Email ?? student.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
