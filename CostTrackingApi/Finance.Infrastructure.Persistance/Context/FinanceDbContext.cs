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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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


            var expenses = new List<Expense>()
            {
                new Expense () {Id = 1, Date = DateTime.UtcNow ,Description = " Expense type 1" , Amount = 325.33M, ReferenceId = 188, Type = ExpenseType.Maintenance, MaintenanceRecordId = 11},
                new Expense () {Id = 2, Date = DateTime.UtcNow ,Description = " Expense type 2" , Amount = 325.33M, ReferenceId = 126, Type = ExpenseType.Salary, ConstructionSiteId = 7},
            };
            builder.Entity<Expense>().HasData(expenses);
        }
    }
}
