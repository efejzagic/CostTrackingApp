﻿// <auto-generated />
using System;
using Finance.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Finance.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(FinanceDbContext))]
    [Migration("20230816230537_Finance init")]
    partial class Financeinit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Finance.Domain.Entities.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("ArticleId")
                        .HasColumnType("integer");

                    b.Property<int>("ConstructionSiteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MachineryId")
                        .HasColumnType("integer");

                    b.Property<int>("MaintenanceRecordId")
                        .HasColumnType("integer");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("integer");

                    b.Property<int>("ToolId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Expense");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 325.33m,
                            ArticleId = 0,
                            ConstructionSiteId = 0,
                            Date = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7811),
                            Description = " Expense type 1",
                            MachineryId = 0,
                            MaintenanceRecordId = 11,
                            ReferenceId = 188,
                            ToolId = 0,
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 325.33m,
                            ArticleId = 0,
                            ConstructionSiteId = 7,
                            Date = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7823),
                            Description = " Expense type 2",
                            MachineryId = 0,
                            MaintenanceRecordId = 0,
                            ReferenceId = 126,
                            ToolId = 0,
                            Type = 0
                        });
                });

            modelBuilder.Entity("Finance.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("ArticleId")
                        .HasColumnType("integer");

                    b.Property<int>("ConstructionSiteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MachineryId")
                        .HasColumnType("integer");

                    b.Property<int>("MaintenanceRecordId")
                        .HasColumnType("integer");

                    b.Property<int>("ToolId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Invoice");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1298.92m,
                            ArticleId = 1,
                            ConstructionSiteId = 0,
                            Date = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7600),
                            DueDate = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7604),
                            MachineryId = 0,
                            MaintenanceRecordId = 0,
                            ToolId = 0
                        },
                        new
                        {
                            Id = 2,
                            Amount = 498.92m,
                            ArticleId = 0,
                            ConstructionSiteId = 0,
                            Date = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7624),
                            DueDate = new DateTime(2023, 8, 16, 23, 5, 37, 47, DateTimeKind.Utc).AddTicks(7625),
                            MachineryId = 0,
                            MaintenanceRecordId = 12,
                            ToolId = 0
                        });
                });

            modelBuilder.Entity("Finance.Domain.Entities.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("Finance.Domain.Entities.InvoiceItem", b =>
                {
                    b.HasOne("Finance.Domain.Entities.Invoice", null)
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId");
                });

            modelBuilder.Entity("Finance.Domain.Entities.Invoice", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
