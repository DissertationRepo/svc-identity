using IdentityService.Application.AbstractServices;
using IdentityService.Application.Models;

namespace IdentityService.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;

        public LoginService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public string Login(Login login)
        {
            return _tokenService.GenerateToken(login.Email);
        }
    }
}
