using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Learning_ASP_Dot_Net.Services
{
    public class JwtServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public JwtServices(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration; 
        }

        public async Task<LoginResponseModel> AuthenticateUser(LoginRequestModel model)
        {
            if(string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return null;
            
            var student = _dbContext.Students.FirstOrDefault(s => s.Email == model.Email);
            if (student == null || !BCrypt.Net.BCrypt.Verify(model.Password, student.Password))
                return null;

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration["JwtConfig:TokenValidityMins"];
            var tokenExpiry = DateTime.UtcNow.AddMinutes(double.Parse(tokenValidityMins));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("email", student.Email)
                }),
                Expires = tokenExpiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new LoginResponseModel
            {
                Email = student.Email,  
                AccessToken = accessToken,
                ExpiresIn = (int)TimeSpan.FromMinutes(double.Parse(tokenValidityMins)).TotalSeconds
            };
        }
    }
}
