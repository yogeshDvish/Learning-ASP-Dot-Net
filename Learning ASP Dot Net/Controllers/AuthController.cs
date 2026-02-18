using Learning_ASP_Dot_Net.Models;
using Learning_ASP_Dot_Net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Learning_ASP_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private JwtServices _jwtServices;

        public AuthController(JwtServices jwtServices) => _jwtServices = jwtServices;

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var result = await _jwtServices.AuthenticateUser(model);
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });
            return result;
        }
    }
}
