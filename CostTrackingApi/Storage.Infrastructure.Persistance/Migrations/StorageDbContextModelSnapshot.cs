﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Storage.Infrastructure.Persistance.Context;

#nullable disable

namespace Storage.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    partial class StorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Storage.Domain.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("InStock")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("OrderRequired")
                        .HasColumnType("boolean");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<bool>("retired")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Article");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6915),
                            Description = "Cement article",
                            InStock = true,
                            Name = "Cement",
                            OrderRequired = false,
                            Price = 5.0,
                            Quantity = 20,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6924),
                            Description = "Walnut flooring",
                            InStock = true,
                            Name = "Parquet floor",
                            OrderRequired = false,
                            Price = 32.5,
                            Quantity = 13,
                            SupplierId = 2,
                            retired = false
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6926),
                            Description = "Rods",
                            InStock = true,
                            Name = "Iron Reinforcement",
                            OrderRequired = false,
                            Price = 30.0,
                            Quantity = 3,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6929),
                            Description = "Roof brick red",
                            InStock = true,
                            Name = "Brick",
                            OrderRequired = false,
                            Price = 2.7999999999999998,
                            Quantity = 380,
                            SupplierId = 4,
                            retired = false
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6931),
                            Description = "Plexiglas",
                            InStock = false,
                            Name = "Plexiglas",
                            OrderRequired = false,
                            Price = 74.900000000000006,
                            Quantity = 5,
                            SupplierId = 5,
                            retired = false
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6934),
                            Description = "Electric wires 2m",
                            InStock = false,
                            Name = "Electric wires",
                            OrderRequired = false,
                            Price = 2.2999999999999998,
                            Quantity = 120,
                            SupplierId = 6,
                            retired = false
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6936),
                            Description = "Plasterboard 3x4m",
                            InStock = false,
                            Name = "Plasterboard",
                            OrderRequired = false,
                            Price = 14.25,
                            Quantity = 30,
                            SupplierId = 2,
                            retired = false
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6938),
                            Description = "Screw 8mm",
                            InStock = false,
                            Name = "Screw M8",
                            OrderRequired = false,
                            Price = 0.25,
                            Quantity = 1000,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6940),
                            Description = "Screw 10mm",
                            InStock = false,
                            Name = "Screw M10",
                            OrderRequired = false,
                            Price = 0.34999999999999998,
                            Quantity = 570,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 10,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7010),
                            Description = "Floor insulation",
                            InStock = false,
                            Name = "Floor insulation",
                            OrderRequired = false,
                            Price = 112.55,
                            Quantity = 12,
                            SupplierId = 3,
                            retired = false
                        },
                        new
                        {
                            Id = 11,
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7013),
                            Description = "Fiber cement siding",
                            InStock = false,
                            Name = "Fiber cement siding",
                            OrderRequired = false,
                            Price = 17.5,
                            Quantity = 22,
                            SupplierId = 1,
                            retired = false
                        });
                });

            modelBuilder.Entity("Storage.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("OrderComplete")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ShippingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderComplete = false,
                            OrderDate = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7056),
                            ShippingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 0.0
                        },
                        new
                        {
                            Id = 2,
                            OrderComplete = false,
                            OrderDate = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(7060),
                            ShippingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 0.0
                        });
                });

            modelBuilder.Entity("Storage.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("integer");

                    b.Property<string>("ArticleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<double>("PricePerItem")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArticleId = 5,
                            ArticleName = "Plexiglas",
                            OrderId = 1,
                            PricePerItem = 72.0,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 2,
                            ArticleId = 6,
                            ArticleName = "Electric wires",
                            OrderId = 2,
                            PricePerItem = 2.0,
                            Quantity = 100
                        });
                });

            modelBuilder.Entity("Storage.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("retired")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Supplier");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Address",
                            City = "Konjic",
                            Country = "Bosnia and Herzegovina",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6817),
                            Email = "bmaterial@example.com",
                            Name = "Building Material Supplier",
                            Phone = "+387891010",
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            Address = "Address 2",
                            City = "Sarajevo",
                            Country = "Bosnia and Herzegovina",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6827),
                            Email = "wood@example.com",
                            Name = "Wood Supplier",
                            Phone = "+387891011",
                            retired = false
                        },
                        new
                        {
                            Id = 3,
                            Address = "Address",
                            City = "Mostar",
                            Country = "Bosnia and Herzegovina",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6830),
                            Email = "insulation@example.com",
                            Name = "Insulation Supplier",
                            Phone = "+387891012",
                            retired = false
                        },
                        new
                        {
                            Id = 4,
                            Address = "Address",
                            City = "Bihać",
                            Country = "Bosnia and Herzegovina",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6833),
                            Email = "rmaterial@example.com",
                            Name = "Roof Material Supplier",
                            Phone = "+387891013",
                            retired = false
                        },
                        new
                        {
                            Id = 5,
                            Address = "Address",
                            City = "Split",
                            Country = "Croatia",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6835),
                            Email = "gmaterial@example.com",
                            Name = "Glass Material Supplier",
                            Phone = "+385891014",
                            retired = false
                        },
                        new
                        {
                            Id = 6,
                            Address = "Address",
                            City = "Novi Sad",
                            Country = "Serbia",
                            DateCreated = new DateTime(2023, 9, 17, 14, 35, 24, 206, DateTimeKind.Utc).AddTicks(6841),
                            Email = "ematerial@example.com",
                            Name = "Electrical Material Supplier",
                            Phone = "+381891014",
                            retired = false
                        });
                });

            modelBuilder.Entity("Storage.Domain.Entities.Article", b =>
                {
                    b.HasOne("Storage.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Articles")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Storage.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Storage.Domain.Entities.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Storage.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Storage.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
