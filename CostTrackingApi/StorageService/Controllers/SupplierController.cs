using Microsoft.AspNetCore.Mvc;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepo;

        public SupplierController(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierRepo.GetSuppliers();
        }
    }
}
