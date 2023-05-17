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

    }
}
