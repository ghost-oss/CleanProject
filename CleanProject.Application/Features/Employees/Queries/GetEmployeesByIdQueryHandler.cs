using System;
using CleanProject.Application.Models;
using MediatR;

namespace CleanProject.Application.Features.Employees.Queries
{
    public class GetEmployeesByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        public GetEmployeesByIdQueryHandler()
        {
        }

        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var listOfEmployees = new List<EmployeeDTO> { new EmployeeDTO { FirstName = "Sahil" }, new EmployeeDTO { FirstName = "Oscar" } };

            if (request.EmployeeId == 1)
            {
                return listOfEmployees.First(x => x.FirstName == "Sahil");
            }
            else
            {
                return null;
            }
        }
    }
}

