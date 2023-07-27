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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6322),
                            Description = "Desc 1",
                            Name = "Article 1",
                            Price = 10.0,
                            Quantity = 1,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6327),
                            Description = "Desc 2",
                            Name = "Article 2",
                            Price = 20.0,
                            Quantity = 2,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6330),
                            Description = "Desc 3",
                            Name = "Article 3",
                            Price = 30.0,
                            Quantity = 3,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6332),
                            Description = "Desc 4",
                            Name = "Article 4",
                            Price = 40.0,
                            Quantity = 4,
                            SupplierId = 1,
                            retired = false
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6333),
                            Description = "Desc 5",
                            Name = "Article 5",
                            Price = 50.0,
                            Quantity = 5,
                            SupplierId = 2,
                            retired = false
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6337),
                            Description = "Desc 6",
                            Name = "Article 6",
                            Price = 60.0,
                            Quantity = 6,
                            SupplierId = 2,
                            retired = false
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
                            Address = "Address 1",
                            City = "City 1",
                            Country = "Country 1",
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6266),
                            Email = "email1@example.com",
                            Name = "Supplier 1",
                            Phone = "Phone 1",
                            retired = false
                        },
                        new
                        {
                            Id = 2,
                            Address = "Address 2",
                            City = "City 2",
                            Country = "Country 2",
                            DateCreated = new DateTime(2023, 7, 27, 19, 25, 3, 197, DateTimeKind.Utc).AddTicks(6275),
                            Email = "email2@example.com",
                            Name = "Supplier 2",
                            Phone = "Phone 2",
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

            modelBuilder.Entity("Storage.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
