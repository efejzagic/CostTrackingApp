using Microsoft.AspNetCore.Mvc;

using Storage.Application.Features.Supplier.Commands;
using Storage.Application.Features.Supplier.Queries;
using Storage.Application.Parameters.Supplier;
using WebApi.Controllers;

namespace Storage.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class SupplierController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllSupplierParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllSupplierQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSupplierByIdQuery { Id = id }));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetSupplierByNameQuery { Name = name }));
        }

        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        public async Task<IActionResult> Post(CreateSupplierCommand command)
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
        public async Task<IActionResult> Put(UpdateSupplierCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteSupplierCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
