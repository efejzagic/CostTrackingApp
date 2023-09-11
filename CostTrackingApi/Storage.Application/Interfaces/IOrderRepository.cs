using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderItem>> GetItemsByOrderId(int id);

    }
}
