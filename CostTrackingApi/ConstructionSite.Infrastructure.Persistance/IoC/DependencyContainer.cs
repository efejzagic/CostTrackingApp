using ConstructionSite.Application.Interfaces;
using ConstructionSite.Infrastructure.Persistance.Context;
using ConstructionSite.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Infrastructure.Persistance.IoC
{
    //public static class DependencyContainer
    //{
    //    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        services.AddDbContext<EquipmentDbContext>(options =>
    //            options.UseSqlServer(
    //                configuration.GetConnectionString("DbConnection"), o => o.UseNetTopologySuite()));
    //        #region Repositories
    //        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
    //        //services.AddTransient<IEnviromentRepositoryAsync, EnviromentRepositoryAsync>();
    //        #endregion
    //    }
    //    public static void ApplyMigrations(EquipmentDbContext context)
    //    {
    //        try
    //        {
    //            if (context.Database.GetPendingMigrations().Any())
    //            {
    //                context.Database.Migrate();
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //    }

    //}
    public static class DependencyContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConstructionSiteDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("ConstructionSiteConnection")
             ),
             ServiceLifetime.Transient // Assuming you want to use a new instance of DbContext for each request
         );

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient<IEnviromentRepositoryAsync, EnviromentRepositoryAsync>();
            #endregion
        }

        public static void ApplyMigrations(ConstructionSiteDbContext context)
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
                // Handle the exception here if necessary
            }
        }
    }
}
