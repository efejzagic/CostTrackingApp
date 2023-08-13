using EquipmentService.Models;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.DTO.Tool
{
    public class ToolCreateDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public ToolStatus ToolStatus { get; set; }
        [Required]
        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
