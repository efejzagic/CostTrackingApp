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
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

         
            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {
            //Seed Suppliers
            var machinery = new List<Machinery>()
                {
                    new Machinery() { Id=1, Name = "Komatsu PC200 excavator", Description= "SN: KPC200", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                      Location = "Mostar Construction Site", ConstructionSiteId = 1 ,retired = false },
                    new Machinery() { Id=2, Name = "Caterpillar 320d excavator", Description= "SN: C320d", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Sarajevo", ConstructionSiteId = 2 , retired = false },

                    new Machinery() { Id=3, Name = "Volvo EC210D excavator", Description= "SN: VEC210D", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Sarajevo", ConstructionSiteId = 2 , retired = false },
                    new Machinery() { Id=4, Name = "Liebherr 132 hc tower crane", Description= "SN: L132hc", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Sarajevo", ConstructionSiteId = 2 , retired = false },
                    new Machinery() { Id=5, Name = "Schwing concrete pump", Description= "SN: SSP1800", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Mostar", ConstructionSiteId = 1 , retired = false },


                    new Machinery() { Id=6, Name = "Caterpillar asphalt paver", Description= "SN: CAP300", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Konjic", ConstructionSiteId = 3 , retired = false },
                    new Machinery() { Id=7, Name = "Caterpillar dump truck", Description= "SN: C797", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Konjic", ConstructionSiteId = 3 , retired = false },
                    new Machinery() { Id=8, Name = "BOMAG roller compactor", Description= "SN: BAC200", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Konjic", ConstructionSiteId = 3 , retired = false },
                     new Machinery() { Id=9, Name = "MAN concrete roller truck", Description= "SN: MTGS", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Konjic", ConstructionSiteId = 3 , retired = false },
                     new Machinery() { Id=10, Name = "Komatsu bulldozer", Description= "SN: Kd65", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        Location = "Konjic", ConstructionSiteId = 3 , retired = false },


                };

            builder.Entity<Machinery>().HasData(machinery);

            // Seed Articles

            var tools = new List<Tool>()
                {
                    new Tool() { Id=1, Title = "Hammer", Description = "", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=2, Title = "Level", Description = "", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=3, Title = "Chisel", Description = "", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=4, Title = "Mallet", Description = "", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=5, Title = "Screwdriver Set", Description = "Phillips", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=6, Title = "Cordless Dril", Description = "", Location = "Mostar", ConstructionSiteId = 1, retired = false},
                    new Tool() { Id=7, Title = "Jigsaw", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},
                    new Tool() { Id=8, Title = "Heat Gun", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},
                    new Tool() { Id=9, Title = "Table Saw", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},
                    new Tool() { Id=10, Title = "Belt sander", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},
                    new Tool() { Id=11, Title = "Router", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},
                    new Tool() { Id=12, Title = "Hammer", Description = "", Location = "Sarajevo", ConstructionSiteId = 2, retired = false},

                    new Tool() { Id=13, Title = "Utility Bar", Description = "", Location = "Konjic", ConstructionSiteId = 3, retired = false},
                    new Tool() { Id=14, Title = "Screwdriver set", Description = "Flat", Location = "Konjic", ConstructionSiteId = 3, retired = false},
                    new Tool() { Id=15, Title = "Circular Saw", Description = "", Location = "Konjic", ConstructionSiteId = 3, retired = false},
                    new Tool() { Id=16, Title = "Nail Gun", Description = "", Location = "Konjic", ConstructionSiteId = 3, retired = false},
                    new Tool() { Id=17, Title = "Chalk Line", Description = "", Location = "Konjic", ConstructionSiteId = 3, retired = false},
                };

            builder.Entity<Tool>().HasData(tools);



        }
    }
}
