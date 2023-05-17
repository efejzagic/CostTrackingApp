using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly StorageDbContext _context;

        public SupplierRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Supplier.ToListAsync();
        }

        public async Task<Supplier> GetById(int id)
        {
            return await _context.Supplier.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
