using Microsoft.AspNetCore.Mvc;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;
using StorageService.Services;

namespace StorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepo;
        private readonly SupplierService _supplierService;

        public SupplierController(ISupplierRepository supplierRepo,
            SupplierService supplierService)
        {
            _supplierRepo = supplierRepo;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            return await _supplierService.GetSuppliers();
        }
    }
}
