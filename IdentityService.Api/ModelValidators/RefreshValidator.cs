using FluentValidation;
using IdentityService.Api.Models;

namespace IdentityService.Api.ModelValidators
{
    public class RefreshValidator : AbstractValidator<Refresh>
    {
        public RefreshValidator() 
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken is required.");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
