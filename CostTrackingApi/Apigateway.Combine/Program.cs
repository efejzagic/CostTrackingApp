
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
        Title = "ApiGateway"
    });
});
#endregion
builder.Services.AddHealthChecks();
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

#endregion
var app = builder.Build();
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