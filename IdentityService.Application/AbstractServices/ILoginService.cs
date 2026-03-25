using IdentityService.Application.Models;

namespace IdentityService.Application.AbstractServices
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(Login login);
        Task<bool> Register(Register register);
    }
}
