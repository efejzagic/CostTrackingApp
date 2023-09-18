using ResponseInfo.Entities;
using Storage.Application.DTOs.Order;
using Storage.Application.Features.Order.Commands;

namespace Apigateway.Combine.Interfaces
{
    public interface IOrderExpenseService
    {
        Task<Response<OrderDTO>> CreateExpenseByOrder(CreateOrderCommand order);
    }
}
