namespace EquipmentService.DTO.MachineryServicing
{
    public class ToolServicingMDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public int ToolId { get; set; }


        public DateTime ServiceDate { get; set; }

        public bool retired { get; set; }
    }
}
