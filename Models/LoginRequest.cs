using System.ComponentModel.DataAnnotations;

namespace FagestProAdmin.Models
{
    public class LoginRequest:LoginViewModel
    {
      
        public bool RememberMe { get; set; }
    }
}