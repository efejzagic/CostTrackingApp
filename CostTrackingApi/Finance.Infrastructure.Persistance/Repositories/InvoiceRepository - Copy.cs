using Finance.Application.Interfaces;
using Finance.Domain.Entities;
using Finance.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Persistance.Repositories
{
    
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly FinanceDbContext _dbContext;
        public ExpenseRepository(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ExpenseItem>> GetItemsByExpenseId(int id)
        {
            return await _dbContext.ExpenseItems.Where(it => it.ExpenseId == id).ToListAsync();
        }
    }
}
