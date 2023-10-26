using Storage.Application.Interfaces;
using Storage.Infrastructure.Persistance.Context;
using Storage.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Storage.Infrastructure.Persistance.IoC
{
    public static class DependencyContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StorageDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("StorageConnection")
             ),
             ServiceLifetime.Transient 
         );

            #region Repositories

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            #endregion
        }

        public static void ApplyMigrations(StorageDbContext context)
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
