using IdentityService.Application.AbstractServices;
using IdentityService.Application.Models;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public LoginService(
            ITokenService tokenService,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenGenerator = refreshTokenGenerator;
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
            var loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return loginResponse;
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
    }
}
