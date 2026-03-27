using IdentityService.Application.AbstractServices;
using IdentityService.Application.Models;
using IdentityService.Domain.Entities;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenHasher _tokenHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
            ITokenService tokenService,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IRefreshTokenGenerator refreshTokenGenerator,
            ITokenHasher tokenHasher,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenHasher = tokenHasher;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<LoginResponse> Login(Login login)
        {
            var email = Email.Create(login.Email);
            var user = await _userRepository.GetUserByEmailAsync(email.ToString());
            if (user == null || !_passwordHasher.Verify(user.PasswordHash, login.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            
            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            var accessToken = _tokenService.GenerateToken(email.ToString());

            var refreshTokenDomain = CreateRefreshToken(refreshToken, user.Id);
            await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenDomain);

            var loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return loginResponse;
        }

        public async Task Logout(Logout logout)
        {
            var hashedToken = _tokenHasher.Hash(logout.RefreshToken);
            var domainRefreshToken = await _refreshTokenRepository.GetByTokenHashAsync(hashedToken);
            if (domainRefreshToken != null && domainRefreshToken.IsActive(DateTime.UtcNow))
            {
                domainRefreshToken.Revoke(DateTime.UtcNow, "User logged out");
                await _refreshTokenRepository.UpdateRefreshTokenAsync(domainRefreshToken);
            }
        }

        public async Task<bool> Register(Register register)
        {
            var passwordHash = _passwordHasher.Hash(register.Password);
            var user = new Domain.Entities.User(
                register.FirstName,
                register.LastName,
                register.Email,
                passwordHash,
                register.Role
            );
            return await _userRepository.AddUserAsync(user);
        }

        private RefreshToken CreateRefreshToken(string refreshToken, Guid userId)
        {
            var hashedToken = _tokenHasher.Hash(refreshToken);
            var refreshTokenDomain = new RefreshToken(
                    hashedToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(1),
                    userId
                ); 
            return refreshTokenDomain;
        }
    }
}
