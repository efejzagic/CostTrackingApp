using Finance.Infrastructure.Persistance.Context;
using Finance.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Finance.Infrastructure.Persistance.Repositories;

namespace Finance.Infrastructure.Persistance.IoC
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
            services.AddDbContext<FinanceDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("FinanceConnection")
             ),
             ServiceLifetime.Transient // Assuming you want to use a new instance of DbContext for each request
         );

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient(typeof(IInvoiceRepository), typeof(InvoiceRepository));

            //services.AddTransient<IEnviromentRepositoryAsync, EnviromentRepositoryAsync>();
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
                // Handle the exception here if necessary
            }
        }
    }
}
