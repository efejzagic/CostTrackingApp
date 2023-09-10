using Equipment.Application.Features.Machinery.Commands;
using Equipment.Application.Features.Machinery.Queries;
using Equipment.Application.Parameters.Machinery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Equipment.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class MachineryController : BaseApiController
    {

        private readonly ILogger<MachineryController> _logger;
        public MachineryController(ILogger<MachineryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllMachineryParameter filter)
        {
            _logger.LogInformation("Get Machinery Call");
            return Ok(await Mediator.Send(new GetAllMachineryQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMachineryByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetMachineryByNameQuery { Name = name}));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateMachineryCommand command)
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
        public async Task<IActionResult> Put(UpdateMachineryCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteMachineryCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
