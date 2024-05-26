using CleanProject.Application.Features.Employees.Commands;
using CleanProject.Application.Features.Employees.Queries;
using CleanProject.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMediator mediator;
        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
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
            var result = await mediator.Send(new GetEmployeeByIdQuery(id));;
            if(result == null)
            {
                return BadRequest("Employee not found.");
            }

            return Ok(result);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO dto)
        {  
            await mediator.Send(new CreateEmployeeCommand(dto));
            return Ok();
        }
    }
}
