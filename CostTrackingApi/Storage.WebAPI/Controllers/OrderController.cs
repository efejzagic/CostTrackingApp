using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Features.Order.Commands;
using Storage.Application.Features.Order.Queries;
using Storage.Application.Parameters.Order;
using WebApi.Controllers;

namespace Storage.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Roles = "Storage Manager,Finance")]

        public async Task<IActionResult> Get([FromQuery] GetAllOrderParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllOrderQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Storage Manager,Finance")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }



        //[HttpGet("{id}/history")]
        //public async Task<IActionResult> GetHistory(string id)
        //{
        //    return Ok(await Mediator.Send(new GetEnviromentByIdWithHistoryQuery { Id = id }));
        //}

        [HttpPost]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            //var enviroment = await Mediator.Send(command);
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPost("SetComplete")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> SetComplete(SetOrderCompleteCommand command)
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
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Put(UpdateOrderCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Storage Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteOrderCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
