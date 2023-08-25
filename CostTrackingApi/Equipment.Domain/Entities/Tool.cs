using System.ComponentModel.DataAnnotations;

namespace Equipment.Domain.Entities
{
    public class Tool
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }   
        public string Description { get; set; }
   
        [Required]
        public string Location { get; set; }
     

        public bool retired { get; set; }
    }
}
