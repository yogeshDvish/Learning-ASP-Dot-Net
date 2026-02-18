namespace Learning_ASP_Dot_Net.Models
{
    public class LoginResponseModel
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string Password { get; set; }
    }
}
