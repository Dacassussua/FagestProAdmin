using System.ComponentModel.DataAnnotations;

namespace FagestProAdmin.Models
{
    public class RegisterRequest : LoginViewModel
    {
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }
    }
}