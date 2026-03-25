using IdentityService.Application.AbstractServices;
using System.Security.Cryptography;

namespace IdentityService.Infrastructure.Services
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
