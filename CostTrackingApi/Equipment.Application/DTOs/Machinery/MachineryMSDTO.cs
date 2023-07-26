using Equipment.Domain.Entities;

namespace Equipment.Application.DTOs.Machinery
{
    public class MachineryMSDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly ProductionYear { get; set; }
        public MachineryStatus MachineryStatus { get; set; }
        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
