using System;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    {
        public int EmployeeId { get; set; }

        public GetEmployeeByIdQuery(int employeeId)
        {
            this.EmployeeId = employeeId;
        }
    }
}

