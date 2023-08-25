﻿// <auto-generated />
using System;
using Maintenance.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Maintenance.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(MaintenanceDbContext))]
    partial class MaintenanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Maintenance.Domain.Entities.MaintenanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Technician")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MaintenanceRecord");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Record for machine SN2023",
                            EquipmentId = 1,
                            Name = "MR 1",
                            Price = 120.40000000000001,
                            Status = "Completed",
                            Technician = "User 1",
                            Timestamp = new DateTime(2023, 8, 25, 11, 14, 1, 657, DateTimeKind.Utc).AddTicks(1716)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Record for tool SN2021",
                            EquipmentId = 2,
                            Name = "MR 2",
                            Price = 87.150000000000006,
                            Status = "Pending",
                            Technician = "User 2",
                            Timestamp = new DateTime(2023, 8, 25, 11, 14, 1, 657, DateTimeKind.Utc).AddTicks(1772)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
