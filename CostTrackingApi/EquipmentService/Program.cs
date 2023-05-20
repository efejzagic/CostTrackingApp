using AutoMapper;
using EquipmentService.Interfaces;
using EquipmentService.Profiles;
using EquipmentService.Repositories;
using EquipmentService.Services;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EquipmentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EquipmentConnection")));



builder.Services.AddControllers();

builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();

builder.Services.AddScoped<MachineryService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MachineryProfile());

}).CreateMapper());


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
