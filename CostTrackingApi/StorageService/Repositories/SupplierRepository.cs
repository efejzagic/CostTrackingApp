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

        public async Task<Supplier> Create(Supplier supplier)
        {
            _context.Supplier.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;

        }

        public async Task<Supplier> Edit(Supplier supplier)
        {
            var editSupplier = await GetById(supplier.Id);

            if (editSupplier == null)
            {
                return null;
            }

            editSupplier.Name = supplier.Name;
            editSupplier.Address = supplier.Address;
            editSupplier.City = supplier.City;
            editSupplier.Country = supplier.Country;
            editSupplier.Email = supplier.Email;
            editSupplier.Phone = supplier.Phone;
            editSupplier.retired= supplier.retired;

            await _context.SaveChangesAsync();

            return editSupplier;
        }

        public async Task<bool> Delete(int id)
        {
            var supplier = await GetById(id);

            if (supplier == null)
            {
                return false;
            }

            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var supplier = await GetById(id);

            if (supplier == null)
            {
                return false;
            }

            supplier.retired = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
