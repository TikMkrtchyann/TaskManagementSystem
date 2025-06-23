using FluentValidation;
using FluentValidation.Results;

namespace TaskManagement.Shared_UI.Validation.Base
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
       async (model, propertyName) =>
       {
           var context = ValidationContext<T>.CreateWithOptions(
               (T)model,
               x => x.IncludeProperties(propertyName)
           );

           ValidationResult result = await ValidateAsync(context);
           return result.IsValid
               ? Array.Empty<string>()
               : result.Errors.Select(e => e.ErrorMessage);
       };
    }
}
