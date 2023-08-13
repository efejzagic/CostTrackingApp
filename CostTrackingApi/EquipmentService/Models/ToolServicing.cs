using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Models
{
    public class ToolServicing
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int ToolId { get; set; }

        public virtual Tool Tool { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }
    }
}
