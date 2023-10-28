using Equipment.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.Machinery
{
    public class MachineryEditDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public DateTime ProductionYear { get; set; }

        [Required]
        public string Location { get; set; }
        public int? ConstructionSiteId { get; set; }


        public bool retired { get; set; }

    }
}
