using EquipmentService.Models;

namespace EquipmentService.Interfaces
{
    public interface IToolRepository
    {
        Task<IEnumerable<Tool>> GetAll();

        Task<Tool> GetById(int id);

        Task<Tool> GetByName(string name);

        Task<Tool> Create(Tool tool);

        Task<Tool> Edit(Tool tool);

        Task<bool > Delete(int id);
        Task<bool> SoftDelete(int id);
    }
}
