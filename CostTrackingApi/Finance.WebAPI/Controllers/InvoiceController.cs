using Finance.WebAPI.Controllers;
using Finance.Application.Features.Invoice.Commands;
using Finance.Application.Features.Invoice.Queries;
//using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Finance.Application.Parameters.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class InvoiceController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllInvoiceParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllInvoicesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetInvoiceByIdQuery { Id = id }));
        }

        //[HttpGet("name/{name}")]
        //public async Task<IActionResult> Get(string name)
        //{
        //    return Ok(await Mediator.Send(new GetMachineryByNameQuery { Name = name }));
        //}

     
        [HttpPost]
        public async Task<IActionResult> Post(CreateInvoiceCommand command)
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
        public async Task<IActionResult> Put(UpdateInvoiceCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteInvoiceCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
