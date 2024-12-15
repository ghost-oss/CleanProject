using CleanProject.Application.Features.Employees.Commands;
using CleanProject.Application.Features.Employees.Queries;
using CleanProject.Application.Models;
using CleanProject.Domain.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMediator mediator;
        private readonly ILogger logger;
        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }


        [HttpGet, Route("test")]
        public IActionResult GetEmployeesTest()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>
            {
                new EmployeeDTO { FirstName = "Sahil", DateOfBirth = DateTime.Today.ToString()},
                new EmployeeDTO { FirstName = "Richard", DateOfBirth = DateTime.Today.ToString()},
                new EmployeeDTO { FirstName = "Lucas", DateOfBirth = DateTime.Today.ToString()}

            };

            return Ok(employees);
        }


        [HttpGet, Route("")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await mediator.Send(new GetEmployeesQuery());
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        [TypeFilter(typeof(TestFilter))] //https://www.devtrends.co.uk/blog/dependency-injection-in-action-filters-in-asp.net-core
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            logger.LogWarning("About to retrieve employee information...");

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
