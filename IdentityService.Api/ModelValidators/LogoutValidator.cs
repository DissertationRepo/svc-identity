using FluentValidation;
using IdentityService.Api.Models;

namespace IdentityService.Api.ModelValidators
{
    public class LogoutValidator : AbstractValidator<Logout>
    {
        public LogoutValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}
