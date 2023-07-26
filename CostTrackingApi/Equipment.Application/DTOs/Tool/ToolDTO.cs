﻿using Equipment.Application.DTOs.ToolServicing;
using Equipment.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Application.DTOs.Tool
{
    public class ToolDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToolStatus ToolStatus { get; set; }
        public string Location { get; set; }
        public List<ToolServicingMDTO> ServicingHistory { get; set; }
        public List<Domain.Entities.Maintenance> MaintenanceHistory { get; set; }

        public bool retired { get; set; }
    }
}
