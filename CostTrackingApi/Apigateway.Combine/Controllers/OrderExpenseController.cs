using Apigateway.Combine.Interfaces;
using Apigateway.Combine.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResponseInfo.Entities;
using Storage.Application.Features.Order.Commands;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Apigateway.Combine.Controllers
{
    public class OrderExpenseController : BaseApiController
    {
        private readonly IOrderExpenseService _orderExpenseService;
        public OrderExpenseController(IOrderExpenseService orderExpenseService)
        {
            _orderExpenseService = orderExpenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseByOrder(CreateOrderCommand orderCommand)
        {
            var response = await _orderExpenseService.CreateExpenseByOrder(orderCommand);

            if (response != null)
            {
                return Ok(response);
            }

            return StatusCode(500, new { Message = "An error occurred" });
        }
    }
}
