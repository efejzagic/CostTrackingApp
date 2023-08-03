using Equipment.Application.Features.ToolService.Commands;
using Equipment.Application.Features.ToolService.Queries;
using Equipment.Application.Parameters.ToolService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Equipment.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class ToolServiceController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllToolServiceParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllToolServiceQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetToolServiceByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetToolServiceByNameQuery { Name = name}));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateToolServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            //var enviroment = await Mediator.Send(command);
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateToolServiceCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteToolServiceCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
