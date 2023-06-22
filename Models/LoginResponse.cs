namespace FagestProAdmin.Models
{
    public class LoginResponse
    {
        public int userId { get; set; }
        public string activationCode { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
