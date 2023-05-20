using EquipmentService.Interfaces;
using EquipmentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineryController : ControllerBase
    {
        private readonly IMachineryRepository _machineryRepo;

        public MachineryController(IMachineryRepository machineryRepo)
        {
            _machineryRepo = machineryRepo;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<Machinery>> GetAll ()
        {
            return await _machineryRepo.GetAll ();
        }
    }
}
