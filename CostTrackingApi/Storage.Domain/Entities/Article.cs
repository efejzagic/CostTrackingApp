﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Domain.Entities
{
    public class Article : IAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public bool OrderRequired { get; set; } = false;
        public bool InStock { get; set; }

        public Supplier Supplier { get; set; }
        public bool retired { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}
