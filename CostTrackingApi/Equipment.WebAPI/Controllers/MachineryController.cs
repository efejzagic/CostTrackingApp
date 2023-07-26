using Equipment.Application.Features.Machinery.Queries;
using Equipment.Application.Features.Machinery.Commands;
using Equipment.Application.Parameters.Machinery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Equipment.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class MachineryController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllMachineryParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllMachineryQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdQuery { Id = id }));
        //}

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post(CreateEnviromentCommand command)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        //    }
        //    var enviroment = await Mediator.Send(command);            
        //    return Ok(enviroment);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put(UpdateEnviromentCommand command)
        //{
        //    var enviroment = await Mediator.Send(command);
        //    return Ok(enviroment);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var enviroment = await Mediator.Send(new DeleteEnviromentCommand { Id = id });
        //    return Ok(enviroment);
        //}

    }
}
