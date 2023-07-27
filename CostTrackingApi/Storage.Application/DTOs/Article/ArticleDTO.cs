using Storage.Application.DTOs.Supplier;
using Storage.Domain.Entities;
using Equipment.Application.Parameters;

namespace Storage.Application.DTOs.Article
{
    public class ArticleDTO : RequestParameter, IAuditable
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
