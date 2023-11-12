using System;
using CleanProject.Application.Models;
using FluentValidation;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<EmployeeDTO>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.DepartmentId)
                .NotEqual(0)
                .WithMessage("{PropertyName} cannot be 0, please provide a valid department ID");

            RuleFor(x => x.DateOfBirth)
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage("{PropertyName} is an invalid date");
        }
    }
}

