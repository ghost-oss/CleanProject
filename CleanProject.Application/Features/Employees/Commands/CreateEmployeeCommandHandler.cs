using System;
using MediatR;

namespace CleanProject.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        public CreateEmployeeCommandHandler()
        {
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = request.Employee;
            return 1;
        }
    }
}

