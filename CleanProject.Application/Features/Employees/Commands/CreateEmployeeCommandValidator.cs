using System;
using CleanProject.Application.Models;
using FluentValidation;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Employee.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Employee.DepartmentId)
                .NotEqual(0)
                .WithMessage("{PropertyName} cannot be 0, please provide a valid department ID");
        }
    }

    public class CreateEmployeeCommandSValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandSValidator()
        {

            RuleFor(x => x.Employee.DateOfBirth)
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage("{PropertyName} is an invalid date");
        }
    }
}

