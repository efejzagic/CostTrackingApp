using EquipmentService.Models;

namespace EquipmentService.Interfaces
{
    public interface IMachineryRepository
    {
        Task<IEnumerable<Machinery>> GetAll();
    }
}
