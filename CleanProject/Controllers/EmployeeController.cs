using CleanProject.Application.Features.Employees.Commands;
using CleanProject.Application.Features.Employees.Queries;
using CleanProject.Application.Models;
using MediatR;
using CleanProject.Domain.Validator;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMediator mediator;
        public readonly IValidator<EmployeeDTO> validator;
        private readonly IConfiguration configuration;
        public EmployeeController(IMediator mediator, IValidator<EmployeeDTO> validator, IConfiguration config)
        {
            this.mediator = mediator;
            this.validator = validator;
            this.configuration = config;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await mediator.Send(new GetEmployeesQuery());
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var result = await mediator.Send(new GetEmployeeByIdQuery(id));

            if(result == null)
            {
                return BadRequest("Employee not found.");
            }

            return Ok(result);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO dto)
        {
            validator.Validate(dto, true);    
            await mediator.Send(new CreateEmployeeCommand(dto));

            return Ok();
        }
    }
}
