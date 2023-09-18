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
        public DbSet<ExpenseItem> ExpenseItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne()
            .HasForeignKey(ii => ii.InvoiceId);

            modelBuilder.Entity<Expense>()
           .HasMany(i => i.Items)
           .WithOne()
           .HasForeignKey(ii => ii.ExpenseId);

            Seed(modelBuilder);
        }


        public static void Seed(ModelBuilder builder)
        {

            //Seed Invoices

            var invoices = new List<Invoice>()
            {
                new Invoice() {Id = 1 , Date = DateTime.UtcNow , DueDate = DateTime.UtcNow, Amount = 1298.92M, OrderId = 1},
            };

            builder.Entity<Invoice>().HasData(invoices);


            var invoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem() {Id = 1, Amount = 500 , Description = "Cement" , InvoiceId = 1},
                new InvoiceItem() {Id = 2, Amount = 350 , Description = "Parquet floor" , InvoiceId = 1},
                new InvoiceItem() {Id = 3, Amount = 448.92M , Description = "Bricks" , InvoiceId = 1},

            };

            builder.Entity<InvoiceItem>().HasData(invoiceItems);


            var expenses = new List<Expense>()
            {
                new Expense() {Id = 1 , Date = DateTime.UtcNow ,Description = "Mostar Olymic pool expenses", Amount = 1059.12M, ReferenceId = 1112,  ConstructionSiteId = 1},
                new Expense() {Id = 2 , Date = DateTime.UtcNow , Description = "Tool purchase", Amount = 87.8M, ReferenceId = 132, ToolId = 2},
                new Expense() {Id = 3 , Date = DateTime.UtcNow , Description = "Circular saw service", Amount = 188.07M, ReferenceId = 132, MaintenanceRecordId = 4}
            };

            builder.Entity<Expense>().HasData(expenses);


            var expenseItems= new List<ExpenseItem>()
            {
                new ExpenseItem() {Id = 1, Amount = 112 , Description = "Fuel for Komatsu PC200 excavator" , ExpenseId = 1},
                new ExpenseItem() {Id = 2, Amount = 212.8M , Description = "Fuel for Caterpillar 320d excavator" , ExpenseId = 1},
                new ExpenseItem() {Id = 3, Amount = 723.2M, Description = "Food" , ExpenseId = 1},
                new ExpenseItem() {Id = 4, Amount = 10.5M , Description = "Hydraulic fluid for Komatsu PC200 excavator" , ExpenseId = 1},
                new ExpenseItem() {Id = 5, Amount = 11.12M , Description = "Renting dump truck" , ExpenseId = 1},
                new ExpenseItem() {Id = 6, Amount = 87.8M , Description = "Mallet purchase" , ExpenseId = 2},
                new ExpenseItem() {Id = 7, Amount = 87.15M , Description = "Circular Saw" , ExpenseId = 3},
                new ExpenseItem() {Id = 8, Amount = 100.92M , Description = "Oil" , ExpenseId= 3}

            };

            builder.Entity<ExpenseItem>().HasData(expenseItems);
        }
    }
}
