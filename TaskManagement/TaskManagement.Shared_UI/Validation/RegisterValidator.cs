using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared_UI.Validation.Base;

namespace TaskManagement.Shared_UI.Validation
{
    public class RegisterValidator : BaseValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
