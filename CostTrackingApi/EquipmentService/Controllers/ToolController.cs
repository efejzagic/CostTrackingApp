using EquipmentService.DTO.Tool;
using EquipmentService.Models;
using EquipmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly ToolService _toolService;
        public ToolController(ToolService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<ToolDTO>> GetAll ()
        {
            return await _toolService.GetAll();
        }

        [HttpGet("ById/{id}")]
        public async Task<ToolDTO> GetById([FromRoute]int id) 
        {
            return await _toolService.GetById(id);
        }

        [HttpGet("ByTitle/{title}")]
        public async Task<ToolDTO> GetByTitle([FromRoute] string title)
        {
            return await _toolService.GetByTitle(title);
        }

        [HttpPost("Create")]
        public async Task<ToolDTO> Create (ToolCreateDTO tool)
        {
            return await _toolService.Create(tool);
        }

        [HttpPut("Edit")]
        public async Task<ToolDTO> Edit(ToolEditDTO tool)
        {
            return await _toolService.Edit(tool);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool > Delete ([FromRoute] int id)
        {
            return await _toolService.Delete(id);
        }


        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute] int id)
        {
            return await _toolService.SoftDelete(id);
        }

    }
}
