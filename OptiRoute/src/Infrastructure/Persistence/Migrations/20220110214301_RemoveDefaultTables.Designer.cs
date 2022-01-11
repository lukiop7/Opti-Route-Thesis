﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OptiRoute.Infrastructure.Persistence;

namespace OptiRoute.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220110214301_RemoveDefaultTables")]
    partial class RemoveDefaultTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CustomerRoute", b =>
                {
                    b.Property<int>("CustomersDbId")
                        .HasColumnType("integer");

                    b.Property<int>("RoutesDbId")
                        .HasColumnType("integer");

                    b.HasKey("CustomersDbId", "RoutesDbId");

                    b.HasIndex("RoutesDbId");

                    b.ToTable("CustomerRoute");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.BenchmarkInstance", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BestDistance")
                        .HasColumnType("double precision");

                    b.Property<double>("BestVehicles")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("DbId");

                    b.ToTable("BenchmarkInstances");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.BenchmarkResult", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BenchmarkInstanceDbId")
                        .HasColumnType("integer");

                    b.Property<int?>("BestSolutionDbId")
                        .HasColumnType("integer");

                    b.Property<int>("SolutionDbId")
                        .HasColumnType("integer");

                    b.HasKey("DbId");

                    b.HasIndex("BenchmarkInstanceDbId")
                        .IsUnique();

                    b.HasIndex("BestSolutionDbId")
                        .IsUnique();

                    b.HasIndex("SolutionDbId")
                        .IsUnique();

                    b.ToTable("BenchmarkResults");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Demand")
                        .HasColumnType("integer");

                    b.Property<int>("DueDate")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("ReadyTime")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceTime")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("DbId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Depot", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DueDate")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("DbId");

                    b.ToTable("Depots");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Route", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DepotDbId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("SolutionDbId")
                        .HasColumnType("integer");

                    b.HasKey("DbId");

                    b.HasIndex("DepotDbId");

                    b.HasIndex("SolutionDbId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Solution", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DepotDbId")
                        .HasColumnType("integer");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<bool>("Feasible")
                        .HasColumnType("boolean");

                    b.Property<double>("Time")
                        .HasColumnType("double precision");

                    b.HasKey("DbId");

                    b.HasIndex("DepotDbId")
                        .IsUnique();

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("CustomerRoute", b =>
                {
                    b.HasOne("OptiRoute.Domain.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptiRoute.Domain.Entities.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.BenchmarkResult", b =>
                {
                    b.HasOne("OptiRoute.Domain.Entities.BenchmarkInstance", "BenchmarkInstance")
                        .WithOne("BenchmarkResult")
                        .HasForeignKey("OptiRoute.Domain.Entities.BenchmarkResult", "BenchmarkInstanceDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptiRoute.Domain.Entities.Solution", "BestSolution")
                        .WithOne("BestBenchmarkResult")
                        .HasForeignKey("OptiRoute.Domain.Entities.BenchmarkResult", "BestSolutionDbId");

                    b.HasOne("OptiRoute.Domain.Entities.Solution", "Solution")
                        .WithOne("BenchmarkResult")
                        .HasForeignKey("OptiRoute.Domain.Entities.BenchmarkResult", "SolutionDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BenchmarkInstance");

                    b.Navigation("BestSolution");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Route", b =>
                {
                    b.HasOne("OptiRoute.Domain.Entities.Depot", "Depot")
                        .WithMany("Routes")
                        .HasForeignKey("DepotDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptiRoute.Domain.Entities.Solution", "Solution")
                        .WithMany("Routes")
                        .HasForeignKey("SolutionDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Depot");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Solution", b =>
                {
                    b.HasOne("OptiRoute.Domain.Entities.Depot", "Depot")
                        .WithOne("Solution")
                        .HasForeignKey("OptiRoute.Domain.Entities.Solution", "DepotDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Depot");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.BenchmarkInstance", b =>
                {
                    b.Navigation("BenchmarkResult");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Depot", b =>
                {
                    b.Navigation("Routes");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("OptiRoute.Domain.Entities.Solution", b =>
                {
                    b.Navigation("BenchmarkResult");

                    b.Navigation("BestBenchmarkResult");

                    b.Navigation("Routes");
                });
#pragma warning restore 612, 618
        }
    }
}
