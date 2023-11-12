using System;
using CleanProject.Application.Abstractions.Persistance;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        public IEmployeeRepository employeeRepository { get; set; }

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployeeById(request.EmployeeId);

            if(employee is null)
            {
                return null;
            }

            return new EmployeeDTO
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth.ToString(),
                DepartmentId = employee.DepartmentId
            };
        }
    }
}

