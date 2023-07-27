using Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Equipment.Infrastructure.Persistance.Context
{
    public class EquipmentDbContext : DbContext
    {
        public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) : base(options)
        {

        }

        public DbSet<Machinery> Machinery { get; set; }
        public DbSet<Tool> Tool { get; set; }
        public DbSet<MachineryServicing> MachineryServicing { get; set; }
        public DbSet<ToolServicing> ToolServicing { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder
              .Entity<Machinery>()
              .HasMany(p => p.ServicingHistory)
              .WithOne(p => p.Machinery!)
              .HasForeignKey(p => p.MachineryId);



            modelBuilder
              .Entity<MachineryServicing>()
              .HasOne(p => p.Machinery)
              .WithMany(p => p.ServicingHistory)
              .HasForeignKey(p => p.MachineryId);

            modelBuilder
             .Entity<Tool>()
             .HasMany(p => p.ServicingHistory)
             .WithOne(p => p.Tool!)
             .HasForeignKey(p => p.ToolId);

            modelBuilder
              .Entity<ToolServicing>()
              .HasOne(p => p.Tool)
              .WithMany(p => p.ServicingHistory)
              .HasForeignKey(p => p.ToolId);


            modelBuilder
           .Entity<Machinery>()
           .HasMany(p => p.MaintenanceHistory)
           .WithOne(p => p.Machinery!)
           .HasForeignKey(p => p.MachineryId);



            modelBuilder
              .Entity<Maintenance>()
              .HasOne(p => p.Machinery)
              .WithMany(p => p.MaintenanceHistory)
              .HasForeignKey(p => p.MachineryId);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {
            //Seed Suppliers
            var machinery = new List<Machinery>()
                {
                    new Machinery() { Id=1, Name = "Machinery 1", Description= "Description 1", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        MachineryStatus = MachineryStatus.IDLE, Location = "Loc 1", retired = false },
                    new Machinery() { Id=2, Name = "Machinery 2", Description= "Description 2", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        MachineryStatus = MachineryStatus.WORKING, Location = "Loc 2", retired = false },
                };

            builder.Entity<Machinery>().HasData(machinery);

            // Seed Articles

            var tools = new List<Tool>()
                {
                    new Tool() { Id=1, Title = "Tool 1", Description = "Desc 1", Location = "Loc 1", ToolStatus = ToolStatus.FUNCTIONAL , retired = false},
                    new Tool() { Id=2, Title = "Tool 2", Description = "Desc 2", Location = "Loc 2", ToolStatus = ToolStatus.IN_USE, retired = false},

                };

            builder.Entity<Tool>().HasData(tools);


            var machineryServicing = new List<MachineryServicing>()
                {
                    new MachineryServicing() { Id=1, Title = "Machine Service 1", Description = "Desc 1",Price = 10.0, MachineryId = 1, ServiceDate = DateTime.UtcNow, retired = false},
                    new MachineryServicing() { Id=2, Title = "Machine Serivce 2", Description = "Desc 2", Price = 20.5, MachineryId = 2, ServiceDate = DateTime.UtcNow, retired = false},

                };

            builder.Entity<MachineryServicing>().HasData(machineryServicing);

            var toolServicing = new List<ToolServicing>()
                {
                    new ToolServicing() { Id=1, Title = "Tool Service 1", Description = "Desc 1",Price = 3.0, ToolId = 1, ServiceDate = DateTime.UtcNow, retired = false},
                    new ToolServicing() { Id=2, Title = "Tool Serivce 2", Description = "Desc 2", Price = 12.5, ToolId = 2, ServiceDate = DateTime.UtcNow, retired = false},

                };

            builder.Entity<ToolServicing>().HasData(toolServicing);



        }
    }
}
