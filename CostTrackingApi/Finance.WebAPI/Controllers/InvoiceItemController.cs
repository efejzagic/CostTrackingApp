﻿using Finance.WebAPI.Controllers;
using Finance.Application.Features.InvoiceItem.Commands;
//using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Finance.Application.Parameters.InvoiceItem;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Features.InvoiceItem.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    [Authorize(Roles = "Finance")]
    public class InvoiceItemController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllInvoiceItemParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllInvoiceItemsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetInvoiceItemByIdQuery { Id = id }));
        }


     
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
