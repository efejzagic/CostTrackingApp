using Equipment.Application.DTOs.MachineryService;
using Equipment.Domain.Entities;

namespace Equipment.Application.DTOs.Tool
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
