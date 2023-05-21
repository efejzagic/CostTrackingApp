using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Models;

namespace EquipmentService.DTO.Machinery
{
    public class MachineryMSDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly ProductionYear { get; set; }
        public MachineryStatus MachineryStatus { get; set; }
       
        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
