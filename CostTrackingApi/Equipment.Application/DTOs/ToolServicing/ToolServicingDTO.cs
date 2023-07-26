﻿using Equipment.Application.DTOs.Tool;

namespace Equipment.Application.DTOs.ToolServicing
{
    public class ToolServicingDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public int ToolId { get; set; }

        public ToolTSDTO Tool { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }

    }
}
