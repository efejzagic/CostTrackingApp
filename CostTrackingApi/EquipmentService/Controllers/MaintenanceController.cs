using EquipmentService.DTO.MachineryServicing;
using EquipmentService.DTO.Maintenance;
using EquipmentService.Models;
using EquipmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{
    [Route("api/e/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {

        private readonly MaintenanceService _maintenanceService;
        public MaintenanceController(MaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<MaintenanceDTO>> GetAll()
        {
            return await _maintenanceService.GetAll();
        }

        [HttpGet("ById/{id}")]
        public async Task<MaintenanceDTO> GetById([FromRoute] int id)
        {
            return await _maintenanceService.GetById(id);
        }


        [HttpGet("ByTitle/{title}")]
        public async Task<MaintenanceDTO> GetByTitle([FromRoute] string title)
        {
            return await _maintenanceService.GetByTitle(title);
        }



        [HttpPost("Create")]
        public async Task<MaintenanceDTO> Create(Maintenance model)
        {
            return await _maintenanceService.Create(model);
        }

        [HttpPut("Edit")]
        public async Task<MaintenanceDTO> Edit(Maintenance model)
        {
            return await _maintenanceService.Edit(model);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _maintenanceService.Delete(id);
        }

        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _maintenanceService.SoftDelete(id);
        }




    }
}
