using System.ComponentModel.DataAnnotations;

namespace EquipmentService.DTO.MachineryServicing
{
    public class ToolServicingCreateDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int ToolId { get; set; }

        public DateTime ServiceDate { get; set; } = DateTime.Now;

        public bool retired { get; set; }

    }
}
