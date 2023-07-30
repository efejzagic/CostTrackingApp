using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Models;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.DTO.Tool
{
    public class ToolDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToolStatus ToolStatus { get; set; }
        public string Location { get; set; }
        public List<ToolServicingMDTO> ServicingHistory { get; set; }
        public List<Models.Maintenance> MaintenanceHistory { get; set; }

        public bool retired { get; set; }
    }
}
