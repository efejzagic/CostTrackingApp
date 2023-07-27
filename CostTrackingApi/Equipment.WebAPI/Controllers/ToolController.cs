using Equipment.Application.Features.Tool.Queries;
using Equipment.Application.Features.Tool.Commands;
using Equipment.Application.Parameters.Tool;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Equipment.Application.Features.Tool.Commands;

namespace Equipment.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class ToolController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllToolParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllToolQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetToolByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetToolByNameQuery { Name = name}));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateToolCommand command)
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
        public async Task<IActionResult> Put(UpdateToolCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteToolCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
