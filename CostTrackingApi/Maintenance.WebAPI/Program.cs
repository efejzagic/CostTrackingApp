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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    // Automatic registration of validators in assembly
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
                needAutoCreateTable: true) // Create the table if it doesn't exist
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
//builder.Services.AddApplication();
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





builder.Services.AddAutoMapper(typeof(MediatorClass)); // Register AutoMapper

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
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "CostTrackingApi");
    //c.RoutePrefix = string.Empty;
});
#endregion

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
//app.ApplyMigrations(builder.Configuration.)
app.UseHealthChecks("/health");
app.Run();