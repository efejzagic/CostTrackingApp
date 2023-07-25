using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Models
{
    public class Maintenance
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime MaintenanceDate { get; set; }   
        public bool retired { get; set; }
    }
}
