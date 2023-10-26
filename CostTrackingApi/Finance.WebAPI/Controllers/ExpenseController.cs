using Finance.WebAPI.Controllers;
using Finance.Application.Features.Expense.Commands;
using Finance.Application.Features.Expense.Queries;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Parameters.Expense;
using Microsoft.AspNetCore.Authorization;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    
    public class ExpenseController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> Get([FromQuery] GetAllExpenseParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllExpensesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetExpenseByIdQuery { Id = id }));
        }


     
        [HttpPost]
        [Authorize(Roles = "Finance,Storage Manager")]
        public async Task<IActionResult> Post(CreateExpenseCommand command)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpPut]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> Put(UpdateExpenseCommand command)
        {
            var enviroment = await Mediator.Send(command);
            return Ok(enviroment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Finance")]
        public async Task<IActionResult> Delete(int id)
        {
            var enviroment = await Mediator.Send(new DeleteExpenseCommand { Id = id });
            return Ok(enviroment);
        }

    }
}
