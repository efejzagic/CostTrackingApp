using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Equipment.Domain.Entities
{
    public class Machinery
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [NotMapped]
        public DateOnly ProductionYear { get; set; }
    
        [Required]
        public string Location { get; set; }

    
        public bool retired { get; set; }

    }
}
