using Equipment.Application.Parameters;
using Equipment.Domain.Entities;


namespace Equipment.Application.DTOs.Machinery
{
    public class MachineryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly ProductionYear { get; set; }

        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
