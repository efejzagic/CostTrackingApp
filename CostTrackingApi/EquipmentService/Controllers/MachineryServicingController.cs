using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{

    [Route("api/e/[controller]")]
    [ApiController]
    public class MachineryServicingController : ControllerBase
    {
        private readonly MachineryServicingService _machineryServicingService;
        public MachineryServicingController(MachineryServicingService machineryServicingService)
        {
            _machineryServicingService = machineryServicingService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<MachineryServicingDTO>> GetAll()
        {
            return await _machineryServicingService.GetAll();
        }

        [HttpGet("ById/{id}")]
        public async Task<MachineryServicingDTO> GetById([FromRoute] int id)
        {
            return await _machineryServicingService.GetById(id);
        }


        [HttpGet("ByTitle/{title}")]
        public async Task<MachineryServicingDTO> GetByTitle([FromRoute] string title)
        {
            return await _machineryServicingService.GetByTitle(title);
        }



        [HttpPost("Create")]
        public async Task<MachineryServicingDTO> Create(MachineryServicingCreateDTO model)
        {
            return await _machineryServicingService.Create(model);
        }

        [HttpPut("Edit")]
        public async Task<MachineryServicingDTO> Edit(MachineryServicingEditDTO model)
        {
            return await _machineryServicingService.Edit(model);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _machineryServicingService.Delete(id);
        }

        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _machineryServicingService.SoftDelete(id);
        }




    }
}
