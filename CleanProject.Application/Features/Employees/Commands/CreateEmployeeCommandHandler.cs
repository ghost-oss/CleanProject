using System;
using CleanProject.Domain.Interfaces;
using MediatR;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
    {
        public IEmployeeRepository employeeRepository { get; set; }

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = request.Employee;

            await employeeRepository.CreateEmployee(new Domain.Entities.Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = DateTime.Parse(employee.DateOfBirth),
                DepartmentId = employee.DepartmentId
            });
        }
    }
}

