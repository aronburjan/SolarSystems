﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarSystems.Models;

#nullable disable

namespace SolarSystems.Migrations
{
    [DbContext(typeof(SolarSystemsDbContext))]
    [Migration("20230414134743_foreignkeynullable")]
    partial class foreignkeynullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComponentProject", b =>
                {
                    b.Property<int>("ComponentsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("ComponentsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ComponentProject");
                });

            modelBuilder.Entity("SolarSystems.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("componentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("maxStack")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("SolarSystems.Models.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("containerColumn")
                        .HasColumnType("int");

                    b.Property<int>("containerNumber")
                        .HasColumnType("int");

                    b.Property<int>("containerRow")
                        .HasColumnType("int");

                    b.Property<int>("freeSpace")
                        .HasColumnType("int");

                    b.Property<int>("quantityInContainer")
                        .HasColumnType("int");

                    b.Property<int>("totalSpace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("SolarSystems.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("SolarSystems.Models.ProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectStatus");
                });

            modelBuilder.Entity("SolarSystems.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("accessLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ComponentProject", b =>
                {
                    b.HasOne("SolarSystems.Models.Component", null)
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolarSystems.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SolarSystems.Models.Container", b =>
                {
                    b.HasOne("SolarSystems.Models.Component", "Component")
                        .WithMany("Containers")
                        .HasForeignKey("ComponentId");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("SolarSystems.Models.Project", b =>
                {
                    b.HasOne("SolarSystems.Models.User", null)
                        .WithMany("Projects")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SolarSystems.Models.ProjectStatus", b =>
                {
                    b.HasOne("SolarSystems.Models.Project", null)
                        .WithMany("ProjectStatuses")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("SolarSystems.Models.Component", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("SolarSystems.Models.Project", b =>
                {
                    b.Navigation("ProjectStatuses");
                });

            modelBuilder.Entity("SolarSystems.Models.User", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
