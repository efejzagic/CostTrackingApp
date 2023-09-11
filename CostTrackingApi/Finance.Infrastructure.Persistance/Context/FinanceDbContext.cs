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
                new Expense() {Id = 1 , Date = DateTime.UtcNow ,Description = "Description 1", Amount = 1298.92M, ReferenceId = 1112,  ConstructionSiteId = 1},
                new Expense() {Id = 2 , Date = DateTime.UtcNow , Description = "Description 2", Amount = 498.92M, ReferenceId = 132, ToolId = 2}
            };

            builder.Entity<Expense>().HasData(expenses);


            var expenseItems= new List<ExpenseItem>()
            {
                new ExpenseItem() {Id = 1, Amount = 112 , Description = "Expense item 1" , ExpenseId = 1},
                new ExpenseItem() {Id = 2, Amount = 212.8M , Description = "Expense item 2" , ExpenseId = 1},
                new ExpenseItem() {Id = 3, Amount = 723.2M, Description = "Expense item 3" , ExpenseId = 1},
                new ExpenseItem() {Id = 4, Amount = 10.5M , Description = "Expense item 4" , ExpenseId = 1},
                new ExpenseItem() {Id = 5, Amount = 11.12M , Description = "Expense item 5" , ExpenseId = 1},
                new ExpenseItem() {Id = 6, Amount = 87.8M , Description = "Expense item 6" , ExpenseId = 2},
                new ExpenseItem() {Id = 7, Amount = 24.3M , Description = "Expense item 7" , ExpenseId = 2},

            };

            builder.Entity<ExpenseItem>().HasData(expenseItems);
        }
    }
}
