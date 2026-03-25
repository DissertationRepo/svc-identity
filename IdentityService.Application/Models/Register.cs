namespace IdentityService.Application.Models
{
    public record Register
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _role;
        private string _clientId;

        public Register(string firstName, string lastName, string email, string password, string role, string clientId)
        {
            _firstName = firstName ?? throw new ArgumentNullException(nameof(FirstName));
            _lastName = lastName ?? throw new ArgumentNullException(nameof(LastName));
            _email = email ?? throw new ArgumentNullException(nameof(Email));
            _password = password ?? throw new ArgumentNullException(nameof(Password));
            _role = role ?? throw new ArgumentNullException(nameof(Role));
            _clientId = clientId ?? throw new ArgumentNullException(nameof(ClientId));
        }

        public string FirstName { get => _firstName; }
        public string LastName { get => _lastName; }
        public string Email { get => _email; }
        public string Password { get => _password; }
        public string Role { get => _role; }
        public string ClientId { get => _clientId; }
    }
}
