using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StorageService.Models;

namespace StorageService.Data
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Article { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Supplier>()
                .HasMany(p => p.Articles)
                .WithOne(p => p.Supplier!)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder
              .Entity<Article>()
              .HasOne(p => p.Supplier)
              .WithMany(p => p.Articles)
              .HasForeignKey(p => p.SupplierId);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {
            //Seed Suppliers
            List<Supplier> suppliers = new List<Supplier>()
                {
                    new Supplier() { Id=1, Name = "Supplier 1", Address = "Address 1", City = "City 1", Country = "Country 1", Email="email1@example.com", Phone = "Phone 1", retired=false },
                    new Supplier() { Id=2, Name = "Supplier 2", Address = "Address 2", City = "City 2", Country = "Country 2", Email="email2@example.com", Phone = "Phone 2", retired=false }
                };

            builder.Entity<Supplier>().HasData(suppliers);

            // Seed Articles

            List<Article> articles = new List<Article>()
                {
                    new Article() { Id=1, Name = "Article 1", Quantity = 1, Price = 10.0, Description = "Desc 1", SupplierId = 1, retired = false},
                    new Article() { Id=2, Name = "Article 2", Quantity = 2, Price = 20.0, Description = "Desc 2", SupplierId = 1, retired = false},
                    new Article() { Id=3, Name = "Article 3", Quantity = 3, Price = 30.0, Description = "Desc 3", SupplierId = 1, retired = false},
                    new Article() { Id=4, Name = "Article 4", Quantity = 4, Price = 40.0, Description = "Desc 4", SupplierId = 1, retired = false},
                    new Article() { Id=5, Name = "Article 5", Quantity = 5, Price = 50.0, Description = "Desc 5", SupplierId = 2, retired = false},
                    new Article() { Id=6, Name = "Article 6", Quantity = 6, Price = 60.0, Description = "Desc 6", SupplierId = 2, retired = false}

                };

            builder.Entity<Article>().HasData(articles);
        }
    }
}
