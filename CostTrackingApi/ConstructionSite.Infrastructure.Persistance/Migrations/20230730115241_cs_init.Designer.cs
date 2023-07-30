﻿// <auto-generated />
using ConstructionSite.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstructionSite.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(ConstructionSiteDbContext))]
    [Migration("20230730115241_cs_init")]
    partial class cs_init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConstructionSite.Domain.Entities.ConstructionSite", b =>
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ConstructionSite");
                });

            modelBuilder.Entity("ConstructionSite.Domain.Entities.Employee", b =>
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

                    b.Property<int>("ConstructionSiteId")
                        .HasColumnType("integer");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("HourlyRate")
                        .HasColumnType("double precision");

                    b.Property<int>("HoursOfWork")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Salary")
                        .HasColumnType("double precision");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ConstructionSiteId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ConstructionSite.Domain.Entities.Employee", b =>
                {
                    b.HasOne("ConstructionSite.Domain.Entities.ConstructionSite", "ConstructionSite")
                        .WithMany("Zaposlenici")
                        .HasForeignKey("ConstructionSiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConstructionSite");
                });

            modelBuilder.Entity("ConstructionSite.Domain.Entities.ConstructionSite", b =>
                {
                    b.Navigation("Zaposlenici");
                });
#pragma warning restore 612, 618
        }
    }
}
