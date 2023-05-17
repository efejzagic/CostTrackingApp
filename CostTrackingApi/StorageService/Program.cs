using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Profiles;
using StorageService.Repositories;
using StorageService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the database context
builder.Services.AddDbContext<StorageDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StorageConnection")));



builder.Services.AddControllers();


builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

//services 
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<SupplierService>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ArticleProfile(provider.GetService<ISupplierRepository>()));
    cfg.AddProfile(new SupplierProfile(provider.GetService<IArticleRepository>()));

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
