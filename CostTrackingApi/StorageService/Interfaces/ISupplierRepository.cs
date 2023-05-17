using StorageService.Models;

namespace StorageService.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetById(int id);
    }
}
