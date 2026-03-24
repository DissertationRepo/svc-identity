namespace IdentityService.Application.Models
{
    public record Login
    {
        private string _email;
        private string _password;
        private string _clientId;

        public Login(string email, string password, string clientId)
        {
            _email = email ?? throw new ArgumentNullException(nameof(Email));
            _password = password ?? throw new ArgumentNullException(nameof(Password));
            _clientId = clientId ?? throw new ArgumentNullException(nameof(ClientId));
        }
        public string Email { get => _email; }
        public string Password { get => _password; }
        public string ClientId { get => _clientId; }
    }
}
