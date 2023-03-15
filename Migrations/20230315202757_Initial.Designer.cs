﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarSystems.Models;

#nullable disable

namespace SolarSystems.Migrations
{
    [DbContext(typeof(SolarSystemsDbContext))]
    [Migration("20230315202757_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("SolarSystems.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("componentName")
                        .HasColumnType("TEXT");

                    b.Property<int>("maxQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("SolarSystems.Models.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComponentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("containerColumn")
                        .HasColumnType("INTEGER");

                    b.Property<int>("containerNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("containerRow")
                        .HasColumnType("INTEGER");

                    b.Property<int>("quantityInContainer")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("SolarSystems.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComponentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("availableComponentQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("neededComponentQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("projectNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("SolarSystems.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("accessLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
