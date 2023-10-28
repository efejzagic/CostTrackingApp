using Equipment.Application.Interfaces;
using Equipment.Infrastructure.Persistance.Context;
using Equipment.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Equipment.Infrastructure.Persistance.IoC
{
   
    public static class DependencyContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EquipmentDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("EquipmentConnection")
             ),
             ServiceLifetime.Transient 
         );

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            #endregion
        }

        public static void ApplyMigrations(EquipmentDbContext context)
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
