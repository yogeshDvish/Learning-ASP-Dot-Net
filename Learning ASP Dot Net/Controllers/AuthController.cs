using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.DTOs;
using Learning_ASP_Dot_Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Learning_ASP_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateStudentDto user)
        {
            if(_context.Students.Any(s => s.Email == user.Email))
                return BadRequest("Email already exists");

            var student = new Student
            {
                Name = user.Name,
                Age = user.Age,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == user.Email);
            if (student == null || !BCrypt.Net.BCrypt.Verify(user.Password, student.Password))
                return Unauthorized("Invalid credentials");
            var token = GenerateJwtToken(student);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Student student)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, student.Name),
                new Claim(ClaimTypes.Email, student.Email) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
