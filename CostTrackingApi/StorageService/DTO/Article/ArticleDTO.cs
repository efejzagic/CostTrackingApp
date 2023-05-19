using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StorageService.Models;
using StorageService.DTO.Supplier;

namespace StorageService.DTO.Article
{
    public class ArticleDTO : IAuditable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }
        public SupplierArticleDTO Supplier { get; set; }
        public bool retired { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
