using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Equipment.Infrastructure.Persistance.Context;

namespace EquipmentService.Repositories
{
    public class ToolServicingRepository : IToolServicingRepository
    {
        private readonly EquipmentDbContext _context;
        public ToolServicingRepository(EquipmentDbContext context)
        {
            _context = context;
        }

        public async Task<ToolService> Create(ToolService entity)
        {
            _context.ToolServicing.Add(entity);
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
            _context.ToolServicing.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ToolService> Edit(ToolService entity)
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
            editEntity.ToolId = entity.ToolId;
            editEntity.retired = entity.retired;

            await _context.SaveChangesAsync();

            return editEntity;
        }

        public async Task<IEnumerable<ToolService>> GetAll()
        {
            return await _context.ToolServicing.ToListAsync();
        }

        public async Task<ToolService> GetById(int id)
        {
            return await _context.ToolServicing.FirstOrDefaultAsync(ms => ms.Id == id);
        }

        public async Task<ToolService> GetByTitle(string title)
        {
            return await _context.ToolServicing.FirstOrDefaultAsync(ms => ms.Title == title);
        }

        public async Task<ToolService> GetByServiceDate(DateTime serviceDate)
        {
            return await _context.ToolServicing.FirstOrDefaultAsync(ms => DateOnly.FromDateTime(ms.ServiceDate) == 
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

        public async Task<IEnumerable<ToolService>> GetServicingByToolId(int toolId)
        {
            return await _context.ToolServicing.Where(ms => ms.ToolId == toolId).ToListAsync();
        }

        public async Task<ToolService> GetByName(string name)
        {
            return await _context.ToolServicing.FirstOrDefaultAsync(ts => ts.Title == name);
        }
    }
}
