﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarSystems.Models;

#nullable disable

namespace SolarSystems.Migrations
{
    [DbContext(typeof(SolarSystemsDbContext))]
    partial class SolarSystemsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComponentProject", b =>
                {
                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("ComponentId", "ProjectId");

                    b.HasIndex("ProjectId");

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

                    b.Property<int>("maxQuantity")
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

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("containerColumn")
                        .HasColumnType("int");

                    b.Property<int>("containerNumber")
                        .HasColumnType("int");

                    b.Property<int>("containerRow")
                        .HasColumnType("int");

                    b.Property<int>("quantityInContainer")
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("availableComponentQuantity")
                        .HasColumnType("int");

                    b.Property<int>("neededComponentQuantity")
                        .HasColumnType("int");

                    b.Property<int>("projectNumber")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Project");
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
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolarSystems.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SolarSystems.Models.Container", b =>
                {
                    b.HasOne("SolarSystems.Models.Component", "Component")
                        .WithMany("Containers")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("SolarSystems.Models.Project", b =>
                {
                    b.HasOne("SolarSystems.Models.User", "User")
                        .WithOne("Project")
                        .HasForeignKey("SolarSystems.Models.Project", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SolarSystems.Models.Component", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("SolarSystems.Models.User", b =>
                {
                    b.Navigation("Project")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
