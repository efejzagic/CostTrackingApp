using FluentValidation.AspNetCore;
using Equipment.Infrastructure.Persistance.Context;
using Equipment.Infrastructure.Persistance.IoC;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Equipment.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Equipment.Infrastructure.Persistance.Repositories;
using Equipment.WebAPI.Settings;
using Equipment.Application.Mappings;
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
        Title = "Equipment API"
    });
});
#endregion
builder.Services.AddHealthChecks();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCorrelationIdManager();
builder.Services.AddCustomJwtAuthentication();


var logger = new LoggerConfiguration()
 .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: "Host=equipment-db;Port=5432;Database=eqdb;Username=postgres;Password=password;",
                tableName: "Logs",
                needAutoCreateTable: true) 
            .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
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

builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();

builder.Services.AddScoped<IToolRepository, ToolRepository>();


builder.Services.AddAutoMapper(typeof(Equipment.Application.MediatorClass)); 

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MachineryProfile());
    cfg.AddProfile(new ToolProfile());
}).CreateMapper());

builder.Services.AddMediatR(typeof(Equipment.Application.MediatorClass).Assembly);
#endregion
var app = builder.Build();
#region ApplyMigration
RetryHelper.RetryConnection(() =>
{

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<EquipmentDbContext>();
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