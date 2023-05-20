﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StorageService.Data;

#nullable disable

namespace EquipmentService.Migrations
{
    [DbContext(typeof(EquipmentDbContext))]
    partial class EquipmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EquipmentService.Models.Machinery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MachineryStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("ProductionYear")
                        .HasColumnType("date");

                    b.Property<bool>("retired")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Machinery");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            Location = "Loc 1",
                            MachineryStatus = 3,
                            Name = "Machinery 1",
                            ProductionYear = new DateOnly(2023, 5, 20),
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            Location = "Loc 2",
                            MachineryStatus = 0,
                            Name = "Machinery 2",
                            ProductionYear = new DateOnly(2023, 5, 20),
                            retired = false
                        });
                });

            modelBuilder.Entity("EquipmentService.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ToolStatus")
                        .HasColumnType("integer");

                    b.Property<bool>("retired")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Tool");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Desc 1",
                            Location = "Loc 1",
                            Title = "Tool 1",
                            ToolStatus = 0,
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            Description = "Desc 2",
                            Location = "Loc 2",
                            Title = "Tool 2",
                            ToolStatus = 3,
                            retired = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
