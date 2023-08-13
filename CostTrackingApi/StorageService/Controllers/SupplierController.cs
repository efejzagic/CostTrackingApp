using Microsoft.AspNetCore.Mvc;
using StorageService.DTO.Supplier;
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
        [Route("{id}")]
        public async Task<SupplierDTO> GetById([FromRoute] int id)
        {
            return await _supplierService.GetById(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<SupplierDTO> Create(SupplierCreateDTO supplier)
        {
            return await _supplierService.Create(supplier);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<SupplierDTO> Edit(SupplierEditDTO supplier)
        {
            return await _supplierService.Edit(supplier);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _supplierService.Delete(id);
        }

        [HttpPut]
        [Route("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _supplierService.SoftDelete(id);
        }
    }
}
