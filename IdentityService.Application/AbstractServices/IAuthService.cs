using IdentityService.Application.Models;

namespace IdentityService.Application.AbstractServices
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(Login login);
        Task Logout(Logout logout);
        Task<bool> Register(Register register);
    }
}
