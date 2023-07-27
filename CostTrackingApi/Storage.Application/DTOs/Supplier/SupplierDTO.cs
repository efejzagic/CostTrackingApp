using Storage.Application.DTOs.Article;
using System.ComponentModel.DataAnnotations;

namespace Storage.Application.DTOs.Supplier
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

        public List<ArticleSupplierDTO> Articles { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool retired { get; set; }

    }
}
