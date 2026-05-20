using System.Security.Claims;

namespace IdentityService.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string subject);
        string GenerateToken(string subject, Guid userId, string role);
    }
}