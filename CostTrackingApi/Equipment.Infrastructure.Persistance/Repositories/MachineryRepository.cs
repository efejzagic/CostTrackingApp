using Equipment.Application.DTOs;
using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Equipment.Infrastructure.Persistance.Context;

namespace Equipment.Infrastructure.Persistance.Repositories
{
    public class MachineryRepository : IMachineryRepository
    {
        private readonly EquipmentDbContext _context;

        public MachineryRepository(EquipmentDbContext context)
        {
            _context = context;
        }

        public async Task<Machinery> Create(Machinery machinery)
        {
            _context.Machinery.Add(machinery);
            await _context.SaveChangesAsync();
            return machinery;
        }

        public async Task<bool> Delete(int id)
        {
            var machinery = await GetById(id);
            if (machinery == null)
            {
                return false;
            }
            _context.Machinery.Remove(machinery);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Machinery> Edit(Machinery machinery)
        {
            var editMachinery = await GetById(machinery.Id);

            if (editMachinery == null)
            {
                return null;
            }

            editMachinery.Name = machinery.Name;
            editMachinery.Description = machinery.Description;
            editMachinery.ProductionYear = machinery.ProductionYear;
            editMachinery.Location = machinery.Location;
            editMachinery.retired = machinery.retired;

            await _context.SaveChangesAsync();

            return editMachinery;
        }

        public async Task<IEnumerable<Machinery>> GetAll()
        {
            return await _context.Machinery.ToListAsync();
        }

        public async Task<Machinery> GetById(int id)
        {
            return await _context.Machinery.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Machinery> GetByName(string name)
        {
            return await _context.Machinery.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<bool> SoftDelete(int id)
        {
            var machinery = await GetById(id);
            if (machinery == null)
            {
                return false;
            }
            machinery.retired = true;
            await _context.SaveChangesAsync();
            return machinery.retired;
        }
    }
}
