using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Equipment.Infrastructure.Persistance.Context;
using System.Reflection.PortableExecutable;

namespace Equipment.Infrastructure.Persistance.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly EquipmentDbContext _context;
        public ToolRepository(EquipmentDbContext context)
        {
            _context = context;
        }
        public async Task<Tool> Create(Tool tool)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));

            _context.Tool.Add(tool);
            await _context.SaveChangesAsync();
            return tool;
        }

        public async Task<bool> Delete(int id)
        {
            var tool = await GetById(id);

            if (tool == null)
            {
                return false;
            }

            _context.Tool.Remove(tool);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Tool> Edit(Tool tool)
        {
            var editTool = await GetById(tool.Id);

            if (editTool == null)
            {
                return null;
            }

            editTool.Title = tool.Title;
            editTool.Description = tool.Description;
            editTool.Location = tool.Location;
            editTool.retired = tool.retired;

            await _context.SaveChangesAsync();

            return editTool;
        }


        public async Task<IEnumerable<Tool>> GetAll()
        {
            return await _context.Tool.ToListAsync();
        }

        public async Task<Tool> GetById(int id)
        {
            return await _context.Tool.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tool> GetByName(string title)
        {
            return await _context.Tool.FirstOrDefaultAsync(t => t.Title == title);
        }

        public async Task<bool> SoftDelete(int id)
        {
            var tool = await GetById(id);
            if (tool == null)
            {
                return false;
            }
            tool.retired = true;
            await _context.SaveChangesAsync();
            return tool.retired;
        }
    }
}
