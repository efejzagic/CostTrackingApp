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

//builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
//builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IGenericRepositoryAsync<Employee>, GenericRepositoryAsync<Employee>>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


builder.Services.AddAutoMapper(typeof(ConstructionSite.Application.MediatorClass)); // Register AutoMapper

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    //cfg.AddProfile(new ArticleProfile(provider.GetService<ISupplierRepository>()));

    
    cfg.AddProfile(new ConstructionSiteProfile(provider.GetService<IEmployeeRepository>()));
    var constructionRepo = provider.GetService<IGenericRepositoryAsync<ConstructionSite.Domain.Entities.ConstructionSite>>();

    cfg.AddProfile(new EmployeeProfile(constructionRepo));
    //cfg.AddProfile(new SupplierProfile(provider.GetService<IArticleRepository>()));
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