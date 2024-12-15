using System;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDTO>>
    {
    }
}

