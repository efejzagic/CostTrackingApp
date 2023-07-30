using Equipment.Application.DTOs.MachineryService;
using Equipment.Application.Parameters;
using Equipment.Domain.Entities;


namespace Equipment.Application.DTOs.Machinery
{
    public class MachineryDTO : RequestParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly ProductionYear { get; set; }
        public MachineryStatus MachineryStatus { get; set; }

        public List<MachineryServiceMDTO> ServicingHistory { get; set; }
        public List<Domain.Entities.Maintenance> MaintenanceHistory { get; set; }

        public string Location { get; set; }
        public bool retired { get; set; }
    }
}
