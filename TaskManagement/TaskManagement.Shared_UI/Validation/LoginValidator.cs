using FluentValidation;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared_UI.Validation.Base;

namespace TaskManagement.Shared_UI.Validation
{
    public class LoginValidator : BaseValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
