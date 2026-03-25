using IdentityService.Application.AbstractServices;

namespace IdentityService.Infrastructure.Services
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hashed password is required.", nameof(hashedPassword));

            if (string.IsNullOrWhiteSpace(providedPassword))
                throw new ArgumentException("Provided password is required.", nameof(providedPassword));

            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
