using IdentityService.Application.AbstractServices;
using IdentityService.Application.Common;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Infrastructure.Services
{
    public class TokenHasher : ITokenHasher
    {

        private readonly string _secret;

        public TokenHasher(IOptions<RefreshTokenHashingSettings> options)
        {
            _secret = options.Value.Secret;

            if (string.IsNullOrWhiteSpace(_secret))
                throw new InvalidOperationException("Refresh token hashing secret is missing.");
        }
        public string Hash(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token is required.", nameof(token));

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secret));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(hashBytes);
        }
    }
}
