using Finance.Infrastructure.Persistance.Context;
using Finance.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Finance.Infrastructure.Persistance.Repositories;

namespace Finance.Infrastructure.Persistance.IoC
{
    
    public static class DependencyContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("FinanceConnection")
             ),
             ServiceLifetime.Transient 
         );

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
           
            #endregion
        }

        public static void ApplyMigrations(FinanceDbContext context)
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
