using EquipmentService.Interfaces;
using EquipmentService.Models;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using System.Reflection.PortableExecutable;

namespace EquipmentService.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly EquipmentDbContext _context;
        public MaintenanceRepository(EquipmentDbContext context)
        {
            _context = context;
        }
        public async Task<Maintenance> Create(Maintenance entity)
        {
            _context.Maintenance.Add(entity);
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
            _context.Maintenance.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Maintenance> Edit(Maintenance entity)
        {
            var editEntity = await GetById(entity.Id);

            if (editEntity == null)
            {
                return null;
            }

            editEntity.Title = entity.Title;
            editEntity.Description = entity.Description;
            editEntity.Price = entity.Price;
            editEntity.MaintenanceDate = entity.MaintenanceDate;
            editEntity.retired = entity.retired;

            await _context.SaveChangesAsync();

            return editEntity;
        }

        public async Task<IEnumerable<Maintenance>> GetAll()
        {
            return await _context.Maintenance.ToListAsync();
        }

        public async Task<Maintenance> GetById(int id)
        {
            return await _context.Maintenance.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Maintenance> GetByTitle(string title)
        {
            return await _context.Maintenance.FirstOrDefaultAsync(m => m.Title == title);
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
        }
    }
