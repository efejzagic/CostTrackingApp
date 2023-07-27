using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Equipment.Infrastructure.Persistance.Context;

namespace EquipmentService.Repositories
{
    public class MachineryServicingRepository : IMachineryServicingRepository
    {
        private readonly EquipmentDbContext _context;
        public MachineryServicingRepository(EquipmentDbContext context)
        {
            _context = context;
        }

        public async Task<MachineryService> Create(MachineryService entity)
        {
            _context.MachineryServicing.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }
            _context.MachineryServicing.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MachineryService> Edit(MachineryService entity)
        {
            var editEntity = await GetById(entity.Id);

            if (editEntity == null)
            {
                return null;
            }

            editEntity.Title = entity.Title;
            editEntity.Description = entity.Description;
            editEntity.ServiceDate = entity.ServiceDate;
            editEntity.Price = entity.Price;
            editEntity.MachineryId = entity.MachineryId;
            editEntity.retired = entity.retired;

            await _context.SaveChangesAsync();

            return editEntity;
        }

        public async Task<IEnumerable<MachineryService>> GetAll()
        {
            return await _context.MachineryServicing.ToListAsync();
        }

        public async Task<MachineryService> GetById(int id)
        {
            return await _context.MachineryServicing.FirstOrDefaultAsync(ms => ms.Id == id);
        }

        public async Task<MachineryService> GetByTitle(string title)
        {
            return await _context.MachineryServicing.FirstOrDefaultAsync(ms => ms.Title == title);
        }

        public async Task<MachineryService> GetByServiceDate(DateTime serviceDate)
        {
            return await _context.MachineryServicing.FirstOrDefaultAsync(ms => DateOnly.FromDateTime(ms.ServiceDate) == 
                                                                                    DateOnly.FromDateTime(serviceDate));
        }

        public async Task<bool> SoftDelete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }
            entity.retired = true;
            await _context.SaveChangesAsync();
            return entity.retired;

        }

        public async Task<IEnumerable<MachineryService>> GetServicingByMachineryId(int machineryId)
        {
            return await _context.MachineryServicing.Where(ms => ms.MachineryId == machineryId).ToListAsync();
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenanceByMachineryId(int machineryId)
        {
            return await _context.Maintenance.Where(m => m.MachineryId == machineryId).ToListAsync();
        }

        public async Task<MachineryService> GetByName(string name)
        {
            return await _context.MachineryServicing.FirstOrDefaultAsync(ms => ms.Title == name);
        }
    }
}
