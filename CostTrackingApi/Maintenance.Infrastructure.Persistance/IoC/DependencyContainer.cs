using Maintenance.Application.Interfaces;
using Maintenance.Infrastructure.Persistance.Context;
using Maintenance.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maintenance.Infrastructure.Persistance.IoC
{
    
    public static class DependencyContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MaintenanceDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("MaintenanceConnection")
             ),
             ServiceLifetime.Transient 
         );

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            #endregion
        }

        public static void ApplyMigrations(MaintenanceDbContext context)
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
