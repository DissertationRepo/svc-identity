namespace IdentityService.Application.Models
{
    public record Login
    {
        private string _email;
        private string _password;
        private string? _refreshToken;

        public Login(string email, string password, string? refreshToken)
        {
            _email = email ?? throw new ArgumentNullException(nameof(Email));
            _password = password ?? throw new ArgumentNullException(nameof(Password));
            if (refreshToken != null)
            {
                _refreshToken = refreshToken;
            }
        }
        public string Email { get => _email; }
        public string Password { get => _password; }
        public string? RefreshToken { get => _refreshToken; }
    }
}
