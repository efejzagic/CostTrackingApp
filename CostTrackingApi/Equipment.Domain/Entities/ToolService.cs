﻿using System.ComponentModel.DataAnnotations;

namespace Equipment.Domain.Entities
{
    public class ToolService
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int ToolId { get; set; }

        public virtual Tool Tool { get; set; }

        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }
    }
}
