using System.ComponentModel.DataAnnotations;

namespace Equipment.Domain.Entities
{
    public class Tool
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }   
        public string Description { get; set; }
        [Required]
        public ToolStatus ToolStatus { get; set; }
        [Required]
        public string Location { get; set; }
        public ICollection<ToolService> ServicingHistory { get; set; } = new List<ToolService>();
        public ICollection<Maintenance> MaintenanceHistory { get; set; } = new List<Maintenance>();


        public bool retired { get; set; }
    }
}
