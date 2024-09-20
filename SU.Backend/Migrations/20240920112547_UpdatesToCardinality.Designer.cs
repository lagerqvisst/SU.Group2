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
    [Migration("20240920112547_UpdatesToCardinality")]
    partial class UpdatesToCardinality
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SU.Backend.Models.Customers.CompanyCustomer", b =>
                {
                    b.Property<int>("CompanyCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyCustomerId"), 1L, 1);

                    b.Property<string>("CompanyAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyEmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyLandlineNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonPhonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyCustomerId");

                    b.ToTable("CompanyCustomer");
                });

            modelBuilder.Entity("SU.Backend.Models.Customers.PrivateCustomer", b =>
                {
                    b.Property<int>("PrivateCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateCustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrivateCustomerId");

                    b.ToTable("PrivateCustomers");
                });

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

                    b.ToTable("EmployeeRoleAssignments");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", b =>
                {
                    b.Property<int>("InsuranceCoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceCoverageId"), 1L, 1);

                    b.HasKey("InsuranceCoverageId");

                    b.ToTable("InsuranceCoverages");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.LiabilityCoverage", b =>
                {
                    b.Property<int>("LiabilityCoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LiabilityCoverageId"), 1L, 1);

                    b.Property<decimal>("CoverageAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<decimal>("MonthlyPremium")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LiabilityCoverageId");

                    b.HasIndex("InsuranceCoverageId")
                        .IsUnique();

                    b.ToTable("LiabilityCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PrivateCoverage", b =>
                {
                    b.Property<int>("PrivateCoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateCoverageId"), 1L, 1);

                    b.Property<int>("InsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<int>("InsuredPersonId")
                        .HasColumnType("int");

                    b.Property<int>("PrivateCoverageOptionId")
                        .HasColumnType("int");

                    b.HasKey("PrivateCoverageId");

                    b.HasIndex("InsuranceCoverageId")
                        .IsUnique();

                    b.HasIndex("InsuredPersonId");

                    b.ToTable("PrivateCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PrivateCoverageOption", b =>
                {
                    b.Property<int>("PrivateCoverageOptionId")
                        .HasColumnType("int");

                    b.Property<decimal>("CoverageAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MonthlyPremium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PrivateCoverageOptionId");

                    b.ToTable("PrivateCoverageOption");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PropertyAndInventoryCoverage", b =>
                {
                    b.Property<int>("PropertyAndInventoryCoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyAndInventoryCoverageId"), 1L, 1);

                    b.Property<int>("InsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<decimal>("InventoryPremium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InventoryValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PropertyPremium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PropertyValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PropertyAndInventoryCoverageId");

                    b.HasIndex("InsuranceCoverageId")
                        .IsUnique();

                    b.ToTable("PropertyAndInventoryCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.RizkZone", b =>
                {
                    b.Property<int>("RiskZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RiskZoneId"), 1L, 1);

                    b.Property<int>("VehicleCoverageId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleInsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<double>("ZoneFactor")
                        .HasColumnType("float");

                    b.HasKey("RiskZoneId");

                    b.HasIndex("VehicleInsuranceCoverageId");

                    b.ToTable("RizkZone");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.VehicleInsuranceCoverage", b =>
                {
                    b.Property<int>("VehicleInsuranceCoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleInsuranceCoverageId"), 1L, 1);

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Deductible")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<int>("RiskZoneId")
                        .HasColumnType("int");

                    b.HasKey("VehicleInsuranceCoverageId");

                    b.HasIndex("InsuranceCoverageId")
                        .IsUnique();

                    b.HasIndex("RiskZoneId")
                        .IsUnique();

                    b.ToTable("VehicleInsuranceCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Insurance", b =>
                {
                    b.Property<int>("InsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceCoverageId")
                        .HasColumnType("int");

                    b.Property<int>("InsurancePolicyHolderId")
                        .HasColumnType("int");

                    b.Property<string>("InsuranceStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentPlan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("InsuranceId");

                    b.HasIndex("InsuranceCoverageId")
                        .IsUnique();

                    b.HasIndex("InsurancePolicyHolderId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsuranceAddon", b =>
                {
                    b.Property<int>("InsuranceAddonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceAddonId"), 1L, 1);

                    b.Property<int>("InsuranceAddonTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PrivateInsuranceId")
                        .HasColumnType("int");

                    b.HasKey("InsuranceAddonId");

                    b.HasIndex("InsuranceAddonTypeId");

                    b.HasIndex("PrivateInsuranceId");

                    b.ToTable("InsuranceAddons");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsuranceAddonType", b =>
                {
                    b.Property<int>("InsuranceAddonTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceAddonTypeId"), 1L, 1);

                    b.Property<decimal>("BaseExtraPremium")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsuranceAddonTypeId");

                    b.ToTable("InsuranceAddonTypes");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsurancePolicyHolder", b =>
                {
                    b.Property<int>("InsurancePolicyHolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsurancePolicyHolderId"), 1L, 1);

                    b.Property<int?>("CompanyCustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("PrivateCustomerId")
                        .HasColumnType("int");

                    b.HasKey("InsurancePolicyHolderId");

                    b.HasIndex("CompanyCustomerId");

                    b.HasIndex("PrivateCustomerId");

                    b.ToTable("InsurancePolicyHolders");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsuredPerson", b =>
                {
                    b.Property<int>("InsuredPersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuredPersonId"), 1L, 1);

                    b.Property<int?>("InsurancePolicyHolderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsuredPersonId");

                    b.HasIndex("InsurancePolicyHolderId");

                    b.ToTable("InsuredPersons");
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

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.LiabilityCoverage", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", "InsuranceCoverage")
                        .WithOne("LiabilityCoverage")
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.LiabilityCoverage", "InsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InsuranceCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PrivateCoverage", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", "InsuranceCoverage")
                        .WithOne("PrivateCoverage")
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.PrivateCoverage", "InsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SU.Backend.Models.Insurance.InsuredPerson", "InsuredPerson")
                        .WithMany()
                        .HasForeignKey("InsuredPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceCoverage");

                    b.Navigation("InsuredPerson");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PrivateCoverageOption", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.PrivateCoverage", null)
                        .WithOne("PrivateCoverageOption")
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.PrivateCoverageOption", "PrivateCoverageOptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PropertyAndInventoryCoverage", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", "InsuranceCoverage")
                        .WithOne("PropertyAndInventoryCoverage")
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.PropertyAndInventoryCoverage", "InsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InsuranceCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.RizkZone", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.VehicleInsuranceCoverage", "VehicleInsuranceCoverage")
                        .WithMany()
                        .HasForeignKey("VehicleInsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleInsuranceCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.VehicleInsuranceCoverage", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", "InsuranceCoverage")
                        .WithOne("VehicleInsuranceCoverage")
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.VehicleInsuranceCoverage", "InsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SU.Backend.Models.Insurance.Coverage.RizkZone", "RiskZone")
                        .WithOne()
                        .HasForeignKey("SU.Backend.Models.Insurance.Coverage.VehicleInsuranceCoverage", "RiskZoneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InsuranceCoverage");

                    b.Navigation("RiskZone");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Insurance", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", "InsuranceCoverage")
                        .WithOne()
                        .HasForeignKey("SU.Backend.Models.Insurance.Insurance", "InsuranceCoverageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SU.Backend.Models.Insurance.InsurancePolicyHolder", "InsurancePolicyHolder")
                        .WithMany()
                        .HasForeignKey("InsurancePolicyHolderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InsuranceCoverage");

                    b.Navigation("InsurancePolicyHolder");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsuranceAddon", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.InsuranceAddonType", "InsuranceAddonType")
                        .WithMany()
                        .HasForeignKey("InsuranceAddonTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SU.Backend.Models.Insurance.Insurance", "Insurance")
                        .WithMany("InsuranceAddons")
                        .HasForeignKey("PrivateInsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Insurance");

                    b.Navigation("InsuranceAddonType");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsurancePolicyHolder", b =>
                {
                    b.HasOne("SU.Backend.Models.Customers.CompanyCustomer", "CompanyCustomer")
                        .WithMany("InsurancePolicyHolders")
                        .HasForeignKey("CompanyCustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SU.Backend.Models.Customers.PrivateCustomer", "PrivateCustomer")
                        .WithMany("InsurancePolicyHolders")
                        .HasForeignKey("PrivateCustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CompanyCustomer");

                    b.Navigation("PrivateCustomer");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.InsuredPerson", b =>
                {
                    b.HasOne("SU.Backend.Models.Insurance.InsurancePolicyHolder", "InsurancePolicyHolder")
                        .WithMany()
                        .HasForeignKey("InsurancePolicyHolderId");

                    b.Navigation("InsurancePolicyHolder");
                });

            modelBuilder.Entity("SU.Backend.Models.Customers.CompanyCustomer", b =>
                {
                    b.Navigation("InsurancePolicyHolders");
                });

            modelBuilder.Entity("SU.Backend.Models.Customers.PrivateCustomer", b =>
                {
                    b.Navigation("InsurancePolicyHolders");
                });

            modelBuilder.Entity("SU.Backend.Models.Employee.Employee", b =>
                {
                    b.Navigation("RoleAssignments");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.InsuranceCoverage", b =>
                {
                    b.Navigation("LiabilityCoverage");

                    b.Navigation("PrivateCoverage");

                    b.Navigation("PropertyAndInventoryCoverage");

                    b.Navigation("VehicleInsuranceCoverage");
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Coverage.PrivateCoverage", b =>
                {
                    b.Navigation("PrivateCoverageOption")
                        .IsRequired();
                });

            modelBuilder.Entity("SU.Backend.Models.Insurance.Insurance", b =>
                {
                    b.Navigation("InsuranceAddons");
                });
#pragma warning restore 612, 618
        }
    }
}
