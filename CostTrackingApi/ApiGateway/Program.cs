using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using FluentValidation.AspNetCore;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();
app.UseCors();

await app.UseOcelot();

app.UseRouting();

// Other middleware configurations here.

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Ensure that this line is present.
});

app.Run();
