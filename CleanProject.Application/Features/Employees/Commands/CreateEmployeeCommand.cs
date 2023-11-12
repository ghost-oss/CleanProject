using System;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest
    {
        public EmployeeDTO Employee { get; set; }

        public CreateEmployeeCommand(EmployeeDTO employee)
        {
            this.Employee = employee;
        }
    }
}

