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
                    new MaintenanceRecord() { Id=1, EquipmentId = 1, Timestamp = DateTime.UtcNow, Description="Record for machine SN2023", Price = 120.40, Technician = "User 1", Status = "Completed"},
                    new MaintenanceRecord() { Id=2, EquipmentId = 2, Timestamp = DateTime.UtcNow, Description="Record for tool SN2021", Price = 87.15, Technician = "User 2", Status = "Pending"},

                };

            builder.Entity<MaintenanceRecord>().HasData(maintenanceRecord);
        }
    }
}
