using System.ComponentModel.DataAnnotations;

namespace Equipment.Domain.Entities
{
    public class MachineryService
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
        public int MachineryId { get; set; }

        public virtual Machinery Machinery { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }

    }
}
