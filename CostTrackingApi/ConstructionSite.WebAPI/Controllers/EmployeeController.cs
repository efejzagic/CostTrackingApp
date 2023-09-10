using Auth.Application.Features.Auth.Commands;
using Auth.Domain.Entities;
using ConstructionSite.Application.Features.Employee.Commands;
using ConstructionSite.Application.Features.Employee.Queries;
using ConstructionSite.Application.Parameters.Employee;
using Microsoft.AspNetCore.Mvc;


namespace ConstructionSite.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class EmployeeController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllEmployeeParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllEmployeeQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}/{surname}")]
        public async Task<IActionResult> Get(string name, string surname)
        {
            return Ok(await Mediator.Send(new GetEmployeeByNameQuery { Name= name, Surname = surname}));
        }

        ////[HttpGet("{id}/history")]
        ////public async Task<IActionResult> GetHistory(string id)
        ////{
        ////    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        ////}

        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateEmployeeCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
