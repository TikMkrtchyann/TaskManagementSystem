using FluentValidation;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared_UI.Validation.Base;

namespace TaskManagement.Shared_UI.Validation
{
    public class CreateUserTaskValidator : BaseValidator<CreateTaskDto>
    {
        public CreateUserTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be 100 characters or fewer.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must be 500 characters or fewer.");
        }
    }
}
