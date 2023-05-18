﻿using StorageService.Models;
using System.ComponentModel.DataAnnotations;

namespace StorageService.DTO
{
    public class SupplierDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Article> Articles { get; set; }

        public bool retired { get; set; } 

    }
}
