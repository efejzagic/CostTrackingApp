using Storage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Storage.Domain.Entities
{
    public class Supplier : IAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]  
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();

        public bool retired { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}
