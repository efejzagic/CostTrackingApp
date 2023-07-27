using Equipment.Application.Features.MachineryService.Commands;
using Equipment.Application.Features.MachineryService.Queries;
using Equipment.Application.Parameters.Machinery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Equipment.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class MachineryServiceController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllMachineryServiceParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllMachineryServiceQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMachineryServiceByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetMachineryServiceByNameQuery { Name = name}));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateMachineryServiceCommand command)
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
        public async Task<IActionResult> Put(UpdateMachineryServiceCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteMachineryServiceCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
