using FluentValidation;
using FluentValidation.Results;

namespace CleanProject.Domain.Validator
{
    public interface ICommonValidator<TRequest>
    {
        Task<ValidationResult[]> ValidateAsync(TRequest typeToValidate, bool throwOnException);
    }

    public class Validator<TRequest> : ICommonValidator<TRequest> where TRequest : class
    {
       public IEnumerable<IValidator<TRequest>> validators;

        public Validator(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<ValidationResult[]> ValidateAsync(TRequest typeToValidate, bool throwOnException)
        {
            if (!validators.Any())
            {
                return Array.Empty<ValidationResult>();
            }

            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(typeToValidate)));


            if (validationResults.Any(v => !v.IsValid) && throwOnException)
            {
                throw new ValidationException("Failed Validation", validationResults.SelectMany(vr => vr.Errors));
            }

            return validationResults;
        }
        
    }
}

