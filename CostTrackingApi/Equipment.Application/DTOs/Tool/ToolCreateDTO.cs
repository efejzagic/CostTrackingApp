using Equipment.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.Tool
{
    public class ToolCreateDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
