using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Models;
using StorageService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the database context
builder.Services.AddDbContext<StorageDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StorageConnection")));



builder.Services.AddControllers();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
