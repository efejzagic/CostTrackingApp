using Equipment.Domain.Entities;

namespace Equipment.Application.Interfaces
{
    public interface IToolServicingRepository
    {
        Task<IEnumerable<ToolService>> GetAll();

        Task<ToolService> GetById(int id);

        Task<ToolService> GetByTitle(string name);
        Task<ToolService> GetByServiceDate(DateTime serviceDate);

        Task<ToolService> Create(ToolService entity);
        Task<ToolService> Edit(ToolService entity);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);
        Task<IEnumerable<ToolService>> GetServicingByToolId(int id);
        Task<ToolService> GetByName(string name);
    }
}
