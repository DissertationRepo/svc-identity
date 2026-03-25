using FluentValidation;
using IdentityService.Api.Models;

namespace IdentityService.Api.ModelValidators
{
    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Employer" || role == "Candidate")
                .WithMessage("Role must be either 'Employer' or 'Candidate'.");
        }
    }
}
