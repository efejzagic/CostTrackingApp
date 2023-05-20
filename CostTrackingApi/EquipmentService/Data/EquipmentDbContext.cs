﻿using EquipmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace StorageService.Data
{
    public class EquipmentDbContext : DbContext
    {
        public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) : base(options)
        {

        }

        public DbSet<Machinery> Machinery { get; set; }
        public DbSet<Tool> Tool{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        
        public static void Seed(ModelBuilder builder)
        {
            //Seed Suppliers
            var machinery = new List<Machinery>()
                {
                    new Machinery() { Id=1, Name = "Machinery 1", Description= "Description 1", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        MachineryStatus = MachineryStatus.IDLE, Location = "Loc 1", retired = false },
                    new Machinery() { Id=2, Name = "Machinery 2", Description= "Description 2", ProductionYear = DateOnly.FromDateTime(DateTime.Now),
                        MachineryStatus = MachineryStatus.WORKING, Location = "Loc 2", retired = false },
                };

            builder.Entity<Machinery>().HasData(machinery);

            // Seed Articles

           var tools = new List<Tool>()
                {
                    new Tool() { Id=1, Title = "Tool 1", Description = "Desc 1", Location = "Loc 1", ToolStatus = ToolStatus.FUNCTIONAL , retired = false},
                    new Tool() { Id=2, Title = "Tool 2", Description = "Desc 2", Location = "Loc 2", ToolStatus = ToolStatus.IN_USE, retired = false},

                };

            builder.Entity<Tool>().HasData(tools);
        }
    }
}