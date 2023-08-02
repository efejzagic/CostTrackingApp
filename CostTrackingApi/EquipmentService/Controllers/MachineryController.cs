using EquipmentService.DTO.Machinery;
using EquipmentService.Interfaces;
using EquipmentService.Models;
using EquipmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineryController : ControllerBase
    {
        private readonly MachineryService _machineryService;

        public MachineryController(MachineryService machineryService)
        {
            _machineryService = machineryService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<MachineryDTO>> GetAll ()
        {
            return await _machineryService.GetAll();
        }

        [HttpGet("ById/{id}")]
        public async Task<MachineryDTO> GetById([FromRoute]int id)
        {
            return await _machineryService.GetById(id);
        }
        [HttpGet("ByName/{name}")]
        public async Task<MachineryDTO> GetByName([FromRoute] string name)
        {
            return await _machineryService.GetByName(name);
        }

        [HttpPost("Create")]
        public async Task<MachineryDTO> Create (MachineryCreateDTO machinery)
        {
            return await _machineryService.Create(machinery);
        }

        [HttpPut("Edit")]
        public async Task<MachineryDTO> Edit(MachineryEditDTO machinery)
        {
            return await _machineryService.Edit(machinery);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete([FromRoute]int id)
        {
            return await _machineryService.Delete(id);
        }

        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _machineryService.SoftDelete(id);
        }
    }
}
