namespace IdentityService.Application.Models
{
    public record Refresh
    {
        private string _refreshToken;
        private Guid _userId;

        public Refresh(string refreshToken, string userId) 
        { 
            _refreshToken = refreshToken ?? throw new ArgumentNullException(nameof(RefreshToken));
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                throw new ArgumentException("Invalid user id.", nameof(userId));
            }

            _userId = parsedUserId;
        }

        public string RefreshToken { get => _refreshToken; }
        public Guid UserId { get => _userId; }
    }
}
