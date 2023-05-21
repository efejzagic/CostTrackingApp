﻿using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Models
{
    public class Machinery
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public DateOnly ProductionYear { get; set; }
        [Required]
        public MachineryStatus MachineryStatus { get; set; }
        [Required]
        public string Location { get; set; }

        public ICollection<MachineryServicing> ServicingHistory { get; set; } = new List<MachineryServicing>();
        public bool retired { get; set; }

    }
}
