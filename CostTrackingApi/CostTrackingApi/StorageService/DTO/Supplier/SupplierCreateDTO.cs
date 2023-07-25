using System.ComponentModel.DataAnnotations;

namespace StorageService.DTO.Supplier
{
    public class SupplierCreateDTO
    {
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
        public bool retired { get; set; } = false;
    }
}
