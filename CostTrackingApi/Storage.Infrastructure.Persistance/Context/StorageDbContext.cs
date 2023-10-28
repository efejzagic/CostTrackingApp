using Microsoft.EntityFrameworkCore;
using Storage.Domain.Entities;

namespace Storage.Infrastructure.Persistance.Context
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Article { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
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

            modelBuilder.Entity<Order>()
                .HasMany(i => i.OrderItems)
                .WithOne()
                .HasForeignKey(ii => ii.OrderId);


            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {
            //Seed Suppliers
            List<Supplier> suppliers = new List<Supplier>()
                {
                    new Supplier() { Id=1, Name = "Building Material Supplier", Address = "Address", City = "Konjic", Country = "Bosnia and Herzegovina", Email="bmaterial@example.com", Phone = "+387891010", retired=false },
                    new Supplier() { Id=2, Name = "Wood Supplier", Address = "Address 2", City = "Sarajevo", Country = "Bosnia and Herzegovina", Email="wood@example.com", Phone = "+387891011", retired=false },
                    new Supplier() { Id=3, Name = "Insulation Supplier", Address = "Address", City = "Mostar", Country = "Bosnia and Herzegovina", Email="insulation@example.com", Phone = "+387891012", retired=false },
                    new Supplier() { Id=4, Name = "Roof Material Supplier", Address = "Address", City = "Bihać", Country = "Bosnia and Herzegovina", Email="rmaterial@example.com", Phone = "+387891013", retired=false },
                    new Supplier() { Id=5, Name = "Glass Material Supplier", Address = "Address", City = "Split", Country = "Croatia", Email="gmaterial@example.com", Phone = "+385891014", retired=false },
                    new Supplier() { Id=6, Name = "Electrical Material Supplier", Address = "Address", City = "Novi Sad", Country = "Serbia", Email="ematerial@example.com", Phone = "+381891014", retired=false },
                };

            builder.Entity<Supplier>().HasData(suppliers);

            // Seed Articles

            List<Article> articles = new List<Article>()
                {
                    new Article() { Id=1, Name = "Cement", Quantity = 20, Price = 5.0, Description = "Cement article", InStock = true, SupplierId = 1, retired = false},
                    new Article() { Id=2, Name = "Parquet floor", Quantity = 13, Price = 32.50, Description = "Walnut flooring", InStock = true, SupplierId = 2, retired = false},
                    new Article() { Id=3, Name = "Iron Reinforcement", Quantity = 3, Price = 30.0, Description = "Rods", InStock = true, SupplierId = 1, retired = false},
                    new Article() { Id=4, Name = "Brick", Quantity = 380, Price = 2.8, Description = "Roof brick red",InStock = true, SupplierId = 4, retired = false},
                    new Article() { Id=5, Name = "Plexiglas", Quantity = 5, Price = 74.90, Description = "Plexiglas", InStock = false, SupplierId = 5, retired = false},
                    new Article() { Id=6, Name = "Electric wires", Quantity = 120, Price = 2.30, Description = "Electric wires 2m", InStock = false, SupplierId = 6, retired = false},
                    new Article() { Id=7, Name = "Plasterboard", Quantity = 30, Price = 14.25, Description = "Plasterboard 3x4m", InStock = false, SupplierId = 2, retired = false},
                    new Article() { Id=8, Name = "Screw M8", Quantity = 1000, Price = 0.25, Description = "Screw 8mm", InStock = false, SupplierId = 1, retired = false},
                    new Article() { Id=9, Name = "Screw M10", Quantity = 570, Price = 0.35, Description = "Screw 10mm", InStock = false, SupplierId = 1, retired = false},
                    new Article() { Id=10, Name = "Floor insulation", Quantity = 12, Price = 112.55, Description = "Floor insulation", InStock = false, SupplierId = 3, retired = false},
                    new Article() { Id=11, Name = "Fiber cement siding", Quantity = 22, Price = 17.50, Description = "Fiber cement siding", InStock = false, SupplierId = 1, retired = false}

                };

            builder.Entity<Article>().HasData(articles);


            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, OrderDate = DateTime.UtcNow},
                new Order() { Id = 2, OrderDate = DateTime.UtcNow}                
            };

            builder.Entity<Order>().HasData(orders);


            List<OrderItem> orderItems = new List<OrderItem>
            {
                new OrderItem() {Id = 1, ArticleId = 5,ArticleName="Plexiglas", Quantity = 20, PricePerItem = 72.00, OrderId = 1 },
                new OrderItem() {Id = 2, ArticleId = 6,ArticleName = "Electric wires", Quantity = 100, PricePerItem = 2.00, OrderId = 2 }
            };
            builder.Entity<OrderItem>().HasData(orderItems);

        }
    }
}
