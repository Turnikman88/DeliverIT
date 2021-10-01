﻿// <auto-generated />
using System;
using DeliverIT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliverIT.Models.Migrations
{
    [DbContext(typeof(DeliverITDBContext))]
    partial class DeliverITDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CityId" }, "IX_Addresses_CityId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique();

                    b.HasIndex(new[] { "CountryId" }, "IX_Cities_CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AddressId" }, "IX_Customers_AddressId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex(new[] { "AddressId" }, "IX_Employees_AddressId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("DeliverToAddress")
                        .HasColumnType("bit");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("int");

                    b.Property<int>("WareHouseId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Parcels_CategoryId");

                    b.HasIndex(new[] { "CustomerId" }, "IX_Parcels_CustomerId");

                    b.HasIndex(new[] { "ShipmentId" }, "IX_Parcels_ShipmentId");

                    b.HasIndex(new[] { "WareHouseId" }, "IX_Parcels_WareHouseId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationWareHouseId")
                        .HasColumnType("int");

                    b.Property<int>("OriginWareHouseId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DestinationWareHouseId" }, "IX_Shipments_DestinationWareHouseId");

                    b.HasIndex(new[] { "OriginWareHouseId" }, "IX_Shipments_OriginWareHouseId");

                    b.HasIndex(new[] { "StatusId" }, "IX_Shipments_StatusId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.WareHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AddressId" }, "IX_WareHouses_AddressId")
                        .IsUnique();

                    b.ToTable("WareHouses");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Address", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.City", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Customer", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Employee", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.Address", "Address")
                        .WithMany("Employees")
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Parcel", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.Category", "Category")
                        .WithMany("Parcels")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliverIT.Models.DatabaseModels.Customer", "Customer")
                        .WithMany("Parcels")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliverIT.Models.DatabaseModels.Shipment", "Shipment")
                        .WithMany("Parcels")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliverIT.Models.DatabaseModels.WareHouse", "WareHouse")
                        .WithMany("Parcels")
                        .HasForeignKey("WareHouseId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Customer");

                    b.Navigation("Shipment");

                    b.Navigation("WareHouse");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Shipment", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.WareHouse", "DestinationWareHouse")
                        .WithMany("ShipmentDestinationWareHouses")
                        .HasForeignKey("DestinationWareHouseId")
                        .IsRequired();

                    b.HasOne("DeliverIT.Models.DatabaseModels.WareHouse", "OriginWareHouse")
                        .WithMany("ShipmentOriginWareHouses")
                        .HasForeignKey("OriginWareHouseId")
                        .IsRequired();

                    b.HasOne("DeliverIT.Models.DatabaseModels.Status", "Status")
                        .WithMany("Shipments")
                        .HasForeignKey("StatusId")
                        .IsRequired();

                    b.Navigation("DestinationWareHouse");

                    b.Navigation("OriginWareHouse");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.WareHouse", b =>
                {
                    b.HasOne("DeliverIT.Models.DatabaseModels.Address", "Address")
                        .WithMany("WareHouses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Address", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Employees");

                    b.Navigation("WareHouses");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Category", b =>
                {
                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Customer", b =>
                {
                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Shipment", b =>
                {
                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Status", b =>
                {
                    b.Navigation("Shipments");
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.WareHouse", b =>
                {
                    b.Navigation("Parcels");

                    b.Navigation("ShipmentDestinationWareHouses");

                    b.Navigation("ShipmentOriginWareHouses");
                });
#pragma warning restore 612, 618
        }
    }
}
