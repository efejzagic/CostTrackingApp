using ConstructionSite.Application.Features.ConstructionSite.Commands;
using ConstructionSite.Application.Features.ConstructionSite.Queries;
using ConstructionSite.Application.Parameters.ConstructionSite;
using Microsoft.AspNetCore.Mvc;


namespace ConstructionSite.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class ConstructionSiteController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllConstructionSiteParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllConstructionSiteQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetConstructionByIdQuery { Id = id }));
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> Get(string title)
        {
            return Ok(await Mediator.Send(new GetConstructionByNameQuery { Title = title}));
        }

        ////[HttpGet("{id}/history")]
        ////public async Task<IActionResult> GetHistory(string id)
        ////{
        ////    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        ////}

        [HttpPost]
        public async Task<IActionResult> Post(CreateConstructionSiteCommand command)
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
        public async Task<IActionResult> Put(UpdateConstructionSiteCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteConstructionSiteCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
