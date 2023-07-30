using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{

    [Route("api/e/[controller]")]
    [ApiController]
    public class ToolServicingController : ControllerBase
    {
        private readonly ToolServicingService _toolServicingService;
        public ToolServicingController(ToolServicingService toolServicingService)
        {
            _toolServicingService = toolServicingService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<ToolServicingDTO>> GetAll()
        {
            return await _toolServicingService.GetAll();
        }

        [HttpGet("ById/{id}")]
        public async Task<ToolServicingDTO> GetById([FromRoute] int id)
        {
            return await _toolServicingService.GetById(id);
        }


        [HttpGet("ByTitle/{title}")]
        public async Task<ToolServicingDTO> GetByTitle([FromRoute] string title)
        {
            return await _toolServicingService.GetByTitle(title);
        }



        [HttpPost("Create")]
        public async Task<ToolServicingDTO> Create(ToolServicingCreateDTO model)
        {
            return await _toolServicingService.Create(model);
        }

        [HttpPut("Edit")]
        public async Task<ToolServicingDTO> Edit(ToolServicingEditDTO model)
        {
            return await _toolServicingService.Edit(model);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _toolServicingService.Delete(id);
        }

        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _toolServicingService.SoftDelete(id);
        }




    }
}
