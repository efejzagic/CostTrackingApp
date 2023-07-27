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
using Equipment.Application.Mappings;
using EquipmentService.Profiles;
using Equipment.Infrastructure.Persistance.Repositories;
using EquipmentService.Repositories;

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

builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();
builder.Services.AddScoped<IMachineryServicingRepository, MachineryServicingRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<IToolServicingRepository, ToolServicingRepository>();


builder.Services.AddAutoMapper(typeof(Equipment.Application.MediatorClass)); // Register AutoMapper

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MachineryProfile(provider.GetService<IMachineryServicingRepository>()));

    cfg.AddProfile(new MachineryServiceProfile(provider.GetService<IMachineryRepository>()));
    cfg.AddProfile(new MaintenanceProfile());
    cfg.AddProfile(new ToolProfile(provider.GetService<IToolServicingRepository>()));
    cfg.AddProfile(new ToolServicingProfile(provider.GetService<IToolRepository>()));
}).CreateMapper());

builder.Services.AddMediatR(typeof(Equipment.Application.MediatorClass).Assembly);
#endregion
var app = builder.Build();
#region ApplyMigration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EquipmentDbContext>();
    db.Database.Migrate();
}
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