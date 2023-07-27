using Equipment.Domain.Entities;

namespace Equipment.Application.Interfaces
{
    public interface IMachineryServicingRepository
    {
        Task<IEnumerable<MachineryService>> GetAll();

        Task<MachineryService> GetById(int id);

        Task<MachineryService> GetByTitle(string name);
        Task<MachineryService> GetByServiceDate(DateTime serviceDate);

        Task<MachineryService> Create(MachineryService entity);
        Task<MachineryService> Edit(MachineryService entity);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);
        Task<IEnumerable<MachineryService>> GetServicingByMachineryId(int id);
        Task<IEnumerable<Maintenance>> GetMaintenanceByMachineryId(int id);
    }
}
