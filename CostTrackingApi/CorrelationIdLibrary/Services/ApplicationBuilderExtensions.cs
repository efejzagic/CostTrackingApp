using CorrelationIdLibrary.Helpers;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace CorrelationIdLibrary.Services
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder  AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();

    }
}
