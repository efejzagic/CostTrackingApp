using Maintenance.Application.Features.MaintenanceRecord.Commands;
using Maintenance.Application.Features.MaintenanceRecord.Queries;
//using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Maintenance.Application.Parameters.MaintenanceRecord;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class MaintenanceRecordController : BaseApiController
    {
        private readonly ILogger<MaintenanceRecordController> _logger;
        public MaintenanceRecordController(ILogger<MaintenanceRecordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllMaintenanceRecordParameter filter)
        {
            _logger.LogInformation("Get Maintenance Call");
            return Ok(await Mediator.Send(new GetAllMaintenanceRecordQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMaintenanceRecordByIdQuery { Id = id }));
        }

        //[HttpGet("name/{name}")]
        //public async Task<IActionResult> Get(string name)
        //{
        //    return Ok(await Mediator.Send(new GetMachineryByNameQuery { Name = name }));
        //}

     
        [HttpPost]
        public async Task<IActionResult> Post(CreateMaintenanceRecordCommand command)
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
        public async Task<IActionResult> Put(UpdateMaintenanceRecordCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteMaintenanceRecordCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
