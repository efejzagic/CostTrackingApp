using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Parameters;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.MachineryService
{
    public class MachineryServiceDTO : RequestParameter
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public int MachineryId { get; set; }

        public MachineryMSDTO Machinery { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }

    }
}
