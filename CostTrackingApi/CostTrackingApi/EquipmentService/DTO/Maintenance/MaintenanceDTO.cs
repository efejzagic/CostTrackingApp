namespace EquipmentService.DTO.Maintenance
{
    public class MaintenanceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public bool retired { get; set; }
    }
}
