using FluentValidation.AspNetCore;
using Finance.Infrastructure.Persistance.Context;
using Finance.Infrastructure.Persistance.IoC;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Finance.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Finance.Application.Mappings;
using Finance.Infrastructure.Persistance.Repositories;
using Finance.WebAPI.Settings;
using Finance.Application;
using JwtAuthenticationManager;
using Serilog;
using Serilog.Events;
using CorrelationIdLibrary.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

builder.Services.AddCustomJwtAuthentication();


var logger = new LoggerConfiguration()
 .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: "Host=finance-db;Port=5432;Database=fndb;Username=postgres;Password=password;",
                tableName: "Logs",
                needAutoCreateTable: true) 
            .CreateLogger();


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
        Title = "Finance API"
    });
});
#endregion
builder.Services.AddHealthChecks();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddCorrelationIdManager();

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


builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();


builder.Services.AddAutoMapper(typeof(Finance.Application.MediatorClass)); 
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new InvoiceProfile(provider.GetService<IInvoiceRepository>()));
    cfg.AddProfile(new ExpenseProfile(provider.GetService<IExpenseRepository>()));

}).CreateMapper());

builder.Services.AddMediatR(typeof(MediatorClass).Assembly);
#endregion
var app = builder.Build();
#region ApplyMigration
RetryHelper.RetryConnection(() =>
{

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
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