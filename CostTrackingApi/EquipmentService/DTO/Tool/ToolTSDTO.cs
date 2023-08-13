using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Models;

namespace EquipmentService.DTO.Machinery
{
    public class ToolTSDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToolStatus ToolStatus { get; set; }
        public string Location { get; set; }

        public bool retired { get; set; }
    }
}
