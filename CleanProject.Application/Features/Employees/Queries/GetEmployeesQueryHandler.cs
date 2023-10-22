using System;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDTO>>
    {
        public GetEmployeesQueryHandler()
        {

        }

        public async Task<IEnumerable<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            //Here we'd inject our repos / services to handle the logic of actually getting the data
            return new List<EmployeeDTO> { new EmployeeDTO { FirstName = "sahil" } };
        }
    }
}

