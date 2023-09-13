using Finance.Application.Features.Balance;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebAPI.Controllers
{
    //[ApiVersion("1.0")]
    public class BalanceController : BaseApiController
    {
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetBalanceQuery()));
        }

    }
}
