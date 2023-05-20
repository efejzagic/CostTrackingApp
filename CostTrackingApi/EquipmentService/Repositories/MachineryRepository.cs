using EquipmentService.Interfaces;
using EquipmentService.Models;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;

namespace EquipmentService.Repositories
{
    public class MachineryRepository : IMachineryRepository
    {
        private readonly EquipmentDbContext _context;

        public MachineryRepository(EquipmentDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Machinery>> GetAll()
        {
            return await _context.Machinery.ToListAsync();
        }
    }
}
