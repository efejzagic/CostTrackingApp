namespace EquipmentService.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; } 
        public Status Status { get; set; }  
        public string Location { get; set; }
    }
}
