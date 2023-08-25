using Finance.WebAPI.Controllers;
using Finance.Application.Features.Expense.Commands;
using Finance.Application.Features.Expense.Queries;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Parameters.Expense;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class ExpenseController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get([FromQuery] GetAllExpenseParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllExpensesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetExpenseByIdQuery { Id = id }));
        }

        //[HttpGet("name/{name}")]
        //public async Task<IActionResult> Get(string name)
        //{
        //    return Ok(await Mediator.Send(new GetMachineryByNameQuery { Name = name }));
        //}

     
        [HttpPost]
        public async Task<IActionResult> Post(CreateExpenseCommand command)
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
        public async Task<IActionResult> Put(UpdateExpenseCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteExpenseCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
