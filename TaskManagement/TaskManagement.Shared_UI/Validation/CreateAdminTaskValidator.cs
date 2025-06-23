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
    public class CreateAdminTaskValidator : BaseValidator<CreateAdminTaskDto>
    {
        public CreateAdminTaskValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is required.")
               .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must be 500 characters or fewer.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User choice is required")
                .NotNull().WithMessage("User choice is required");

        }
    }
}
