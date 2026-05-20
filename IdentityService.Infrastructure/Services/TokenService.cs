using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Common;
using IdentityService.Application.Services;

namespace IdentityService.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly SigningCredentials _signingCredentials;

        public TokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
            var keyBytes = Encoding.UTF8.GetBytes(_settings.Key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateToken(string subject)
        {
            return GenerateTokenInternal(subject, null, null);
        }

        public string GenerateToken(string subject, Guid userId, string role)
        {
            return GenerateTokenInternal(subject, userId, role);
        }

        private string GenerateTokenInternal(string subject, Guid? userId, string? role)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            if (userId.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()));
                claims.Add(new Claim("uid", userId.Value.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("role", role));
            }

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.ExpiresMinutes),
                signingCredentials: _signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}