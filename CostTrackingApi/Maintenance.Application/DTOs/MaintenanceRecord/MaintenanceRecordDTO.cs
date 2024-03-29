﻿using Maintenance.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.DTOs.MaintenanceRecord
{
    public  class MaintenanceRecordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? MachineryId { get; set; }
        public int? ToolId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public string Technician { get; set; }
        public string Status { get; set; }
    }
}
