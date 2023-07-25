using EquipmentService.Models;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.DTO.Machinery
{
    public class MachineryCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public DateTime ProductionYear { get; set; }
        [Required]
        public MachineryStatus MachineryStatus { get; set; }
        [Required]
        public string Location { get; set; }

        public bool retired { get; set; }
    }
}
