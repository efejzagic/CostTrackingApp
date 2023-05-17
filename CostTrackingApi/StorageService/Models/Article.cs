﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageService.Models
{
    public class Article
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public bool retired { get; set; }

    }
}
