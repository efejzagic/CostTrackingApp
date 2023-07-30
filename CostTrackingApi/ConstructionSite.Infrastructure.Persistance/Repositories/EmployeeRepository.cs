using ConstructionSite.Application.Interfaces;
using ConstructionSite.Domain.Entities;
using ConstructionSite.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Infrastructure.Persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConstructionSiteDbContext _context;

        public EmployeeRepository(ConstructionSiteDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetEmployeesByConstructionId(int constructionId)
        {
            var response = await _context.Employee.Where(a => a.ConstructionSiteId == constructionId).ToListAsync();
            return response;

        }
    }
}
