using System;
using CleanProject.Application.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateEmployeeCommand> context, CancellationToken cancellation = new CancellationToken())
        {
            var data = context.InstanceToValidate.Employee; 
            
            context.AddFailure(new ValidationFailure("test", "test"));
            
            return base.ValidateAsync(context, cancellation);
        }
    }
    
}

