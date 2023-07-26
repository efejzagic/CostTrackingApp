using Equipment.Domain.Entities;

namespace Equipment.Application.Interfaces
{
    public interface IMachineryRepository
    {
        Task<IEnumerable<Machinery>> GetAll();
        Task<Machinery> GetById(int id);

        Task<Machinery> GetByName(string name);

        Task<Machinery> Create(Machinery machinery);

        Task<Machinery> Edit(Machinery machinery);
        Task<bool> Delete(int id);
        Task<bool> SoftDelete(int id);

    }
}
