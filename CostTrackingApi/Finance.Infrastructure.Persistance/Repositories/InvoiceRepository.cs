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
    
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly FinanceDbContext _dbContext;
        public InvoiceRepository(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InvoiceItem>> GetItemsByInvoiceId(int id)
        {
            return await _dbContext.InvoiceItems.Where(it => it.InvoiceId == id).ToListAsync();
        }
    }
}
