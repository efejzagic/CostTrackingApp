using ConstructionSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionSite.Infrastructure.Persistance.Context
{
    public class ConstructionSiteDbContext : DbContext
    {
        public ConstructionSiteDbContext(DbContextOptions<ConstructionSiteDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.Entities.ConstructionSite> ConstructionSite { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder
              .Entity<Domain.Entities.ConstructionSite>()
              .HasMany(p => p.Zaposlenici)
              .WithOne(p => p.ConstructionSite!)
              .HasForeignKey(p => p.ConstructionSiteId);

            modelBuilder
              .Entity<Employee>()
              .HasOne(p => p.ConstructionSite)
              .WithMany(p => p.Zaposlenici)
              .HasForeignKey(p => p.ConstructionSiteId);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {
            //Seed ConstructionSite
            var constructionSite = new List<Domain.Entities.ConstructionSite>()
            {
                new Domain.Entities.ConstructionSite() { Id = 1, Title = "Construction Site 1", Description="Description 1" , Address = "Address 1", City = "City 1", Country = "Country 1"},
                new Domain.Entities.ConstructionSite() { Id = 2, Title = "Construction Site 2", Description="Description 2" , Address = "Address 2", City = "City 2", Country = "Country 2"}
            };
            builder.Entity<Domain.Entities.ConstructionSite>().HasData(constructionSite);

            //Seed employees

            var employees = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "User", Surname="One", Address = "Address 1", City = "City 1", Country = "Country 1", ConstructionSiteId = 1, HourlyRate = 15.5, HoursOfWork = 8, Salary= 2700},
                new Employee() {Id = 2, Name = "Test", Surname="Two", Address = "Address 2", City = "City 2", Country = "Country 2", ConstructionSiteId = 2, HourlyRate = 12.5, HoursOfWork = 7, Salary= 1920.5}
            };
            builder.Entity<Employee>().HasData(employees);

        }
    }
}
