using AutoMapper;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Services
{
    public class SupplierService
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepo;

        public SupplierService(ISupplierRepository supplierRepo,
            IMapper mapper)
        {
            _supplierRepo = supplierRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliers ()
        {
            var suppliers = await _supplierRepo.GetSuppliers();
            return _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<SupplierDTO> GetById(int id)
        {
            var supplier = await _supplierRepo.GetById(id);
            return _mapper.Map<Supplier, SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> Create(Supplier supplier)
        {
            var newSupplier = await _supplierRepo.Create(supplier);
            return _mapper.Map<Supplier, SupplierDTO>(newSupplier);
        }

        public async Task<SupplierDTO> Edit(Supplier supplier)
        {
            supplier.DateModified = DateTime.UtcNow;
            var newSupplier = await _supplierRepo.Edit(supplier);
            return _mapper.Map<Supplier, SupplierDTO>(newSupplier);
        }

        public async Task<bool> Delete(int id)
        {
            return await _supplierRepo.Delete(id);
        }

        public async Task<bool> SoftDelete(int id)
        {
            return await _supplierRepo.SoftDelete(id);
        }

    }
}
