using Equipment.Domain.Entities;

namespace Equipment.Application.Interfaces
{
    public interface IMaintenanceRepository
    {
        Task<IEnumerable<Maintenance>> GetAll();

        Task<Maintenance> GetById(int id);

        Task<Maintenance> GetByTitle(string title);

        Task<Maintenance> Create(Maintenance entity);

        Task<Maintenance> Edit(Maintenance entity);

        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);
    }
}
