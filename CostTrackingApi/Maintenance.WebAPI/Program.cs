using FluentValidation.AspNetCore;
using Maintenance.Infrastructure.Persistance.Context;
using Maintenance.Infrastructure.Persistance.IoC;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Maintenance.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Maintenance.Application.Mappings;
using Maintenance.Application;
using Maintenance.Infrastructure.Persistance.Repositories;
using Maintenance.WebAPI.Settings;
using MediatR;
using Serilog.Events;
using Serilog;
using CorrelationIdLibrary.Services;
using JwtAuthenticationManager;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();


builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });


var logger = new LoggerConfiguration()
 .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: "Host=maintenance-db;Port=5432;Database=mndb;Username=postgres;Password=password;",
                tableName: "Logs",
                needAutoCreateTable: true) 
            .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


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
        Title = "Maintenance API"
    });
});
#endregion
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



builder.Services.AddCorrelationIdManager();


builder.Services.AddAutoMapper(typeof(MediatorClass)); 

builder.Services.AddMediatR(typeof(MediatorClass).Assembly);


#endregion
var app = builder.Build();
#region ApplyMigration
RetryHelper.RetryConnection(() =>
{

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<MaintenanceDbContext>();
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

app.AddCorrelationIdMiddleware();

app.UseAuthorization();

app.MapControllers();
app.UseHealthChecks("/health");
app.Run();