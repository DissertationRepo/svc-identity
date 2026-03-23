namespace IdentityService.Application.Models
{
    public record Profile
    {
        private string _email;
        private string _firstname;
        private string _lastname;
        private string _role;

        public Profile(string email, string firstname, string lastname, string role)
        {
            _email = email ?? throw new ArgumentNullException(nameof(Email));
            _firstname = firstname ?? throw new ArgumentNullException(nameof(FirstName));
            _lastname = lastname ?? throw new ArgumentNullException(nameof(LastName));
            _role = role ?? throw new ArgumentNullException(nameof(Role));
        }

        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Role { get; init; }
    }
}
