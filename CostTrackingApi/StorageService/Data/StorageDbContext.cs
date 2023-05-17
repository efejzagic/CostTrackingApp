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
    }
}
