using FagestProAdmin.Models;
using System.Threading.Tasks;

namespace FagestProAdmin.Services.Interfaces.Login
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }
}