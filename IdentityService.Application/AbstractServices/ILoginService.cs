using IdentityService.Application.Models;

namespace IdentityService.Application.AbstractServices
{
    public interface ILoginService
    {
        string Login(Login login);
    }
}
