using EquipmentService.Interfaces;
using EquipmentService.Repositories;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EquipmentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EquipmentConnection")));



builder.Services.AddControllers();

builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();
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
