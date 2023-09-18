using Finance.WebAPI.Controllers;
using Finance.Application.Features.Invoice.Commands;
using Finance.Application.Features.Invoice.Queries;
//using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Finance.Application.Parameters.Invoice;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Features.InvoiceItem.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Finance.WebAPI.Controllers
{


    //[ApiVersion("1.0")]
    [Authorize(Roles = "Finance")]
    public class InvoiceController : BaseApiController
    {

        private readonly ILogger<InvoiceController> _logger;
        public InvoiceController(ILogger<InvoiceController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        //[MapToApiVersion("1.0")]
        //[Authorize(Roles = "Finance")]
        public async Task<IActionResult> Get([FromQuery] GetAllInvoiceParameter filter)
        {
            _logger.LogInformation("Get Invoice call");
            return Ok(await Mediator.Send(new GetAllInvoicesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetInvoiceByIdQuery { Id = id }));
        }
        [HttpGet("totalAmount")]
        public async Task<IActionResult> GetTotalAmount()
        {
            return Ok(await Mediator.Send(new GetInvoicesAmountQuery()));
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateInvoiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            
            var enviroment = await Mediator.Send(command);

            //var invoiceItems = command.Value.Items;
            //foreach (var item in  invoiceItems)
            //{
            //    item.InvoiceId = enviroment.Data.Id;
            //    await Mediator.Send(new CreateInvoiceItemCommand { Value = item });
            //}

            //var invoiceItemsEnv = await Mediator.Send(new CreateInvoiceItemCommand { Value = invoiceItems });

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
