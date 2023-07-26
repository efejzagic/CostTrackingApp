using Equipment.Domain.Entities;

namespace Equipment.Application.Interfaces
{
    public interface IMachineryServicingRepository
    {
        Task<IEnumerable<MachineryServicing>> GetAll();

        Task<MachineryServicing> GetById(int id);

        Task<MachineryServicing> GetByTitle(string name);
        Task<MachineryServicing> GetByServiceDate(DateTime serviceDate);

        Task<MachineryServicing> Create(MachineryServicing entity);
        Task<MachineryServicing> Edit(MachineryServicing entity);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);
        Task<IEnumerable<MachineryServicing>> GetServicingByMachineryId(int id);
    }
}
