using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Storage.Application.Interfaces;
using Storage.Infrastructure.Persistance.Repositories;
using Storage.Application;
using Storage.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Storage.Infrastructure.Persistance.IoC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Storage.Application.Mappings;
using Storage.WebAPI.Settings;
using JwtAuthenticationManager;
using Microsoft.Extensions.Configuration;
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

//Serilog 
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddCustomJwtAuthentication();
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
        Title = "Storage API"
    });
});
#endregion
builder.Services.AddCors(options =>
{
    var frontendURL = "http://localhost:3000";

    options.AddDefaultPolicy(builder =>
    {
        // builder.WithOrigins(frontendURL!).AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

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

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


builder.Services.AddAutoMapper(typeof(Storage.Application.MediatorClass)); // Register AutoMapper

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ArticleProfile(provider.GetService<ISupplierRepository>()));
    cfg.AddProfile(new SupplierProfile(provider.GetService<IArticleRepository>()));
}).CreateMapper());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatorClass).GetTypeInfo().Assembly));
#endregion
var app = builder.Build();
#region ApplyMigration
RetryHelper.RetryConnection(() =>
{

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<StorageDbContext>();
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
app.UseCors();

app.MapControllers();
//app.ApplyMigrations(builder.Configuration.)
app.UseHealthChecks("/health");
app.Run();