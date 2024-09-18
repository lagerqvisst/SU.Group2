﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SU.Backend.Database;

#nullable disable

namespace SU.Backend.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240915165251_EnumConversionInDb")]
    partial class EnumConversionInDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SU.Backend.Models.Employee.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("AgentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BaseSalary")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ManagerEmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SU.Backend.Models.Employee.EmployeeRoleAssignment", b =>
                {
                    b.Property<int>("EmployeeRoleAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeRoleAssignmentId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<double>("Percentage")
                        .HasColumnType("float");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeRoleAssignmentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeRoleAssignment");
                });

            modelBuilder.Entity("SU.Backend.Models.Employee.Employee", b =>
                {
                    b.HasOne("SU.Backend.Models.Employee.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerEmployeeId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("SU.Backend.Models.Employee.EmployeeRoleAssignment", b =>
                {
                    b.HasOne("SU.Backend.Models.Employee.Employee", "Employee")
                        .WithMany("RoleAssignments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SU.Backend.Models.Employee.Employee", b =>
                {
                    b.Navigation("RoleAssignments");
                });
#pragma warning restore 612, 618
        }
    }
}
