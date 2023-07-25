using EquipmentService.Models;

namespace EquipmentService.Interfaces
{
    public interface IToolServicingRepository
    {
        Task<IEnumerable<ToolServicing>> GetAll();

        Task<ToolServicing> GetById(int id);

        Task<ToolServicing> GetByTitle(string name);
        Task<ToolServicing> GetByServiceDate(DateTime serviceDate);

        Task<ToolServicing> Create(ToolServicing entity);
        Task<ToolServicing> Edit(ToolServicing entity);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);
        Task<IEnumerable<ToolServicing>> GetServicingByToolId(int id);
    }
}
