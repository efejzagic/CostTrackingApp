using Microsoft.EntityFrameworkCore;
using Storage.Application.Interfaces;
using Storage.Domain.Entities;
using Storage.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StorageDbContext _dbContext;
        public OrderRepository(StorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderItem>> GetItemsByOrderId(int id)
        {
            return await _dbContext.OrderItems.Where(it => it.OrderId == id).ToListAsync();
        }

    }
}
