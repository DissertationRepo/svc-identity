using IdentityService.Application.AbstractServices;

namespace IdentityService.Infrastructure.Services
{
    public class TokenHasher : ITokenHasher
    {
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Token is required.", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string hashedToken, string providedToken)
        {
            if (string.IsNullOrWhiteSpace(hashedToken))
                throw new ArgumentException("Hashed password is required.", nameof(hashedToken));

            if (string.IsNullOrWhiteSpace(providedToken))
                throw new ArgumentException("Provided password is required.", nameof(providedToken));

            return BCrypt.Net.BCrypt.Verify(providedToken, hashedToken);
        }
    }
}
