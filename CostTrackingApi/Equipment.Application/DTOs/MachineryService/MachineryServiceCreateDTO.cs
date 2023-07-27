using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.MachineryService
{
    public class MachineryServiceCreateDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int MachineryId { get; set; }

        public DateTime ServiceDate { get; set; } = DateTime.Now;

        public bool retired { get; set; }

    }
}
