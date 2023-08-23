using Finance.WebAPI.Controllers;
using Finance.Application.Features.InvoiceItem.Commands;
//using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Finance.Application.Parameters.Invoice;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Features.InvoiceItem.Queries;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class InvoiceItemController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllInvoiceParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllInvoiceItemsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetInvoiceItemByIdQuery { Id = id }));
        }

        //[HttpGet("name/{name}")]
        //public async Task<IActionResult> Get(string name)
        //{
        //    return Ok(await Mediator.Send(new GetMachineryByNameQuery { Name = name }));
        //}

     
        [HttpPost]
        public async Task<IActionResult> Post(CreateInvoiceItemCommand command)
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
        public async Task<IActionResult> Put(UpdateInvoiceItemCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteInvoiceItemCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
