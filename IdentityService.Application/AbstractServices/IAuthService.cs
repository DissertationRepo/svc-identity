using IdentityService.Application.Models;
using IdentityService.Application.Common;
using IdentityService.Application.Models;

namespace IdentityService.Application.AbstractServices
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> Login(Login login);
        Task Logout(Logout logout);
        Task<Result> Register(Register register);
        Task<Result<RefreshResponse>> Refresh(Refresh refreshCommand);
    }
}
