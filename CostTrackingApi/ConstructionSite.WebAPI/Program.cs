using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Infrastructure.Persistance.Repositories;
using ConstructionSite.Application;
using ConstructionSite.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ConstructionSite.Infrastructure.Persistance.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ConstructionSite.Application.Mappings;
using ConstructionSite.Domain.Entities;
using ConstructionSite.WebAPI.Settings;
using Serilog.Events;
using Serilog;
using CorrelationIdLibrary.Services;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Contact = new OpenApiContact
        {
            Name = "Emil Fejzagic",
            Email = "efejzagic2@etf.unsa.ba",
        },
        Version = "v1",
        Title = "Cost Tracking API"
    });
});
#endregion

var logger = new LoggerConfiguration()
 .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: "Host=constructionsite-db;Port=5432;Database=csdb;Username=postgres;Password=password;",
                tableName: "Logs",
                needAutoCreateTable: true) 
            .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddCorrelationIdManager();

builder.Services.AddHealthChecks();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();

#region API Versioning
//builder.Services.AddApiVersioning(o =>
//{
//    o.AssumeDefaultVersionWhenUnspecified = true;
//    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
//    o.ReportApiVersions = true;
//    o.ApiVersionReader = ApiVersionReader.Combine(
//        new QueryStringApiVersionReader("api-version"),
//        new HeaderApiVersionReader("X-Version"),
//        new MediaTypeApiVersionReader("ver"));
//});

builder.Services.AddScoped<IGenericRepositoryAsync<Employee>, GenericRepositoryAsync<Employee>>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


builder.Services.AddAutoMapper(typeof(ConstructionSite.Application.MediatorClass)); 

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{

    cfg.AddProfile(new ConstructionSiteProfile(provider.GetService<IEmployeeRepository>()));
    var constructionRepo = provider.GetService<IGenericRepositoryAsync<ConstructionSite.Domain.Entities.ConstructionSite>>();

    cfg.AddProfile(new EmployeeProfile(constructionRepo));
}).CreateMapper());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatorClass).GetTypeInfo().Assembly));
#endregion
var app = builder.Build();
#region ApplyMigration
RetryHelper.RetryConnection(() =>
{

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ConstructionSiteDbContext>();
        db.Database.Migrate();
    }
});
#endregion
#region Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{

});
#endregion


app.UseAuthorization();
app.AddCorrelationIdMiddleware();

app.MapControllers();
app.UseHealthChecks("/health");
app.Run();