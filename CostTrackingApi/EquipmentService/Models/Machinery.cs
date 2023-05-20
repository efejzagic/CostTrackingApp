namespace EquipmentService.Models
{
    public class Machinery
    {

        public int Id { get; set; } 
        public string Name { get; set; }

        public string Description { get; set; }
        public DateOnly ProductionYear { get; set; }

        public Status Status { get; set; }
        public string Location { get; set; }

    }
}
