using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StorageService.Models;

namespace StorageService.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }
        public Supplier Supplier { get; set; }
        public bool retired { get; set; }
    }
}
