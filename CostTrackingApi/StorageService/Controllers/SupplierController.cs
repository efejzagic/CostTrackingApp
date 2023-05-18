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
        private readonly SupplierService _supplierService;

        public SupplierController(
            SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [Route("All")]

        public async Task<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            return await _supplierService.GetSuppliers();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<SupplierDTO> GetById(int id)
        {
            return await _supplierService.GetById(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<SupplierDTO> Create(Supplier supplier)
        {
            return await _supplierService.Create(supplier);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<SupplierDTO> Edit(Supplier supplier)
        {
            return await _supplierService.Edit(supplier);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await _supplierService.Delete(id);
        }

        [HttpPut]
        [Route("SoftDelete")]
        public async Task<bool> SoftDelete(int id)
        {
            return await _supplierService.SoftDelete(id);
        }
    }
}
