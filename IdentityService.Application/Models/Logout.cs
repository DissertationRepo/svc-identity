namespace IdentityService.Application.Models
{
    public record Logout
    {
        private string _refreshToken;
        public Logout(string? refreshToken)
        {
            _refreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        public string RefreshToken { get => _refreshToken; }
    }
}
