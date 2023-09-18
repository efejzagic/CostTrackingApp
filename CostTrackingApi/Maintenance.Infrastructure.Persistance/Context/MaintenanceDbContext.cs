using Maintenance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Context
{
    public class MaintenanceDbContext : DbContext
    {
        public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options) : base(options)
        {

        }

        public DbSet<MaintenanceRecord> MaintenanceRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {

            // Seed Articles

            var maintenanceRecord = new List<MaintenanceRecord>()
                {
                    new MaintenanceRecord() { Id=1,Name="Komatsu PC200 maintenance",  MachineryId = 1, Timestamp = DateTime.UtcNow, Description="Service record for Komatsu excavator KPC200", Price = 340.00, Technician = "Emil Fejzagić", Status = "Completed"},
                    new MaintenanceRecord() { Id=2,Name="BOMAG roller compactor",  MachineryId = 8, Timestamp = DateTime.UtcNow, Description="Service record for BOMAG roller compactor BAC200", Price = 120.40, Technician = "Emil Fejzagić", Status = "Pending"},
                    new MaintenanceRecord() { Id=3,Name="Caterpillar AP maintenance",  MachineryId = 6, Timestamp = DateTime.UtcNow, Description="Service record for Caterpillar AP SN: CAP300", Price = 280.90, Technician = "Emil Fejzagić", Status = "Completed"},

                    new MaintenanceRecord() { Id=4, Name="Circular Saw" , ToolId = 2, Timestamp = DateTime.UtcNow, Description="Service record for circular saw", Price = 87.15, Technician = "Mirza Zukanović", Status = "Pending"},
                    new MaintenanceRecord() { Id=5, Name="Cordless Dril" , ToolId = 6, Timestamp = DateTime.UtcNow, Description="Service record for cordless Dril", Price = 12.35, Technician = "Mirza Zukanović", Status = "Completed"},

                };

            builder.Entity<MaintenanceRecord>().HasData(maintenanceRecord);
        }
    }
}
