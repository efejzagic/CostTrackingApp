using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.MachineryServicing
{
    public class MachineryServicingEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int MachineryId { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }

    }
}
