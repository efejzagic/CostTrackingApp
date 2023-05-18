using AutoMapper;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Services
{
    public class SupplierService
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository,
            IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliers ()
        {
            var suppliers = await _supplierRepository.GetSuppliers();
            return _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<SupplierDTO> GetById(int id)
        {
            var supplier = await _supplierRepository.GetById(id);
            return _mapper.Map<Supplier, SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> Create(Supplier supplier)
        {
            var newSupplier = await _supplierRepository.Create(supplier);
            return _mapper.Map<Supplier, SupplierDTO>(newSupplier);
        }

        public async Task<SupplierDTO> Edit(Supplier supplier)
        {
            var newSupplier = await _supplierRepository.Edit(supplier);
            return _mapper.Map<Supplier, SupplierDTO>(newSupplier);
        }

        public async Task<bool> Delete(int id)
        {
            return await _supplierRepository.Delete(id);
        }

        public async Task<bool> SoftDelete(int id)
        {
            return await _supplierRepository.SoftDelete(id);
        }

    }
}
