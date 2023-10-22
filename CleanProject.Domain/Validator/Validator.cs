using System;
using FluentValidation;
using FluentValidation.Results;

namespace CleanProject.Domain.Validator
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T typeToValidate, bool throwOnException);
    }

    public class Validator<T> : IValidator<T> where T : class
    {
        private readonly FluentValidation.IValidator<T> validator;

        public Validator(FluentValidation.IValidator<T> validator)
        {
            this.validator = validator;
        }

        public ValidationResult Validate(T typeToValidate, bool throwOnException)
        {
            var validationResult = validator.Validate(typeToValidate);

            if (!validationResult.IsValid && throwOnException)
            {
                throw new ValidationException("Failed Validation", validationResult.Errors);
            }
            else
            {
                return validationResult;
            }
        }
        
    }
}

