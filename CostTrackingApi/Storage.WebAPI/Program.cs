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
using Serilog.Events;
using CorrelationIdLibrary.Services;
using Storage.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

//Serilog 
var logger = new LoggerConfiguration()
 .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: "Host=storage-db;Port=5432;Database=stgdb;Username=postgres;Password=password;",
                tableName: "Logs",
                needAutoCreateTable: true) 
            .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//correlationId
builder.Services.AddCorrelationIdManager();

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
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddHealthChecks();
builder.Services.AddPersistence(builder.Configuration);
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
builder.Services.AddScoped<IOrderRepository, OrderRepository>();



builder.Services.AddAutoMapper(typeof(Storage.Application.MediatorClass));

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ArticleProfile(provider.GetService<ISupplierRepository>()));
    cfg.AddProfile(new SupplierProfile(provider.GetService<IArticleRepository>()));
    cfg.AddProfile(new OrderProfile(provider.GetService<IOrderRepository>(), provider.GetService<IGenericRepositoryAsync<Article>>()));

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
app.UseSwagger();
app.UseSwaggerUI(c =>
{

});
#endregion


app.AddCorrelationIdMiddleware();

app.UseAuthorization();
app.UseCors();

app.MapControllers();
app.UseHealthChecks("/health");
app.Run();