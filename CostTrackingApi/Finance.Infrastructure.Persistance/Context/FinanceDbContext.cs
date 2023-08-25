using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistance.Context
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {

        }

        public DbSet<Invoice> Invoice{ get; set; }
        public DbSet<Expense> Expense { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne()
            .HasForeignKey(ii => ii.InvoiceId);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {

            //Seed Invoices

            var invoices = new List<Invoice>()
            {
                new Invoice() {Id = 1 , Date = DateTime.UtcNow , DueDate = DateTime.UtcNow, Amount = 1298.92M, ArticleId = 1},
                new Invoice() {Id = 2 , Date = DateTime.UtcNow , DueDate = DateTime.UtcNow, Amount = 498.92M, MaintenanceRecordId = 12}
            };

            builder.Entity<Invoice>().HasData(invoices);


            var invoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem() {Id = 1, Amount = 500 , Description = "Item 1" , InvoiceId = 1},
                new InvoiceItem() {Id = 2, Amount = 350 , Description = "Item 2" , InvoiceId = 1},
                new InvoiceItem() {Id = 3, Amount = 448.92M , Description = "Item 3" , InvoiceId = 1},
                new InvoiceItem() {Id = 4, Amount = 200 , Description = "Item 2.1" , InvoiceId = 2},
                new InvoiceItem() {Id = 5, Amount = 298.92M , Description = "Item 2.2" , InvoiceId = 2}
            };

            builder.Entity<InvoiceItem>().HasData(invoiceItems);


            var expenses = new List<Expense>()
            {
                new Expense () {Id = 1, Date = DateTime.UtcNow ,Description = " Expense type 1" , Amount = 325.33M, ReferenceId = 188, Type = ExpenseType.Maintenance, MaintenanceRecordId = 11},
                new Expense () {Id = 2, Date = DateTime.UtcNow ,Description = " Expense type 2" , Amount = 325.33M, ReferenceId = 126, Type = ExpenseType.Salary, ConstructionSiteId = 7},
            };
            builder.Entity<Expense>().HasData(expenses);
        }
    }
}
