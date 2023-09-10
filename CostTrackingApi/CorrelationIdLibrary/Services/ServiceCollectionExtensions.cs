using CorrelationIdLibrary.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CorrelationIdLibrary.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdManager(this IServiceCollection services)
        {
            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
            return services;
        }

    }
}
