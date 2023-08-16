using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.DTOs.MaintenanceRecord
{
    public class MaintenanceRecordEditDTO
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public string Technician { get; set; }
        public string Status { get; set; }
    }
}
