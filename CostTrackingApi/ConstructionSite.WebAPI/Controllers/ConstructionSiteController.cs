using ConstructionSite.Application.Features.ConstructionSite.Commands;
using ConstructionSite.Application.Features.ConstructionSite.Queries;
using ConstructionSite.Application.Parameters.ConstructionSite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ConstructionSite.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    
    public class ConstructionSiteController : BaseApiController
    {

        private readonly ILogger<ConstructionSiteController> _logger;
        public ConstructionSiteController(ILogger<ConstructionSiteController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Roles = "ConstructionSite Manager,Finance")]

        public async Task<IActionResult> Get([FromQuery] GetAllConstructionSiteParameter filter)
        {
            _logger.LogInformation("Get Construction site call");
            return Ok(await Mediator.Send(new GetAllConstructionSiteQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [Authorize(Roles = "ConstructionSite Manager,Finance")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetConstructionByIdQuery { Id = id }));
        }

        [Authorize(Roles = "ConstructionSite Manager")]
        [HttpGet("title/{title}")]
        public async Task<IActionResult> Get(string title)
        {
            return Ok(await Mediator.Send(new GetConstructionByNameQuery { Title = title}));
        }

        [HttpPost]
        [Authorize(Roles = "ConstructionSite Manager")]
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
        [Authorize(Roles = "ConstructionSite Manager")]
        public async Task<IActionResult> Put(UpdateConstructionSiteCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ConstructionSite Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteConstructionSiteCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
