﻿// <auto-generated />
using System;
using DeliverIT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliverIT.Models.Migrations
{
    [DbContext(typeof(DeliverITDBContext))]
    [Migration("20211001160148_initialcreate")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CityId" }, "IX_Addresses_CityId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            IsDeleted = false,
                            StreetName = "Vasil Levski 14"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            IsDeleted = false,
                            StreetName = "blv. Iztochen 23"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 3,
                            IsDeleted = false,
                            StreetName = "blv. Halic 12"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 4,
                            IsDeleted = false,
                            StreetName = "blv. Zeus 12"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 5,
                            IsDeleted = false,
                            StreetName = "blv. Romunska Morava 1"
                        });
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Shoes"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "Medical supplies"
                        });
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique();

                    b.HasIndex(new[] { "CountryId" }, "IX_Cities_CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            IsDeleted = false,
                            Name = "Sofia"
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 1,
                            IsDeleted = false,
                            Name = "Plovdiv"
                        },
                        new
                        {
                            Id = 3,
                            CountryId = 2,
                            IsDeleted = false,
                            Name = "Istanbul"
                        },
                        new
                        {
                            Id = 4,
                            CountryId = 3,
                            IsDeleted = false,
                            Name = "Athenes"
                        },
                        new
                        {
                            Id = 5,
                            CountryId = 4,
                            IsDeleted = false,
                            Name = "Yash"
                        });
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Bulgaria"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Turkey"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Greece"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "Romania"
                        });
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AddressId" }, "IX_Customers_AddressId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Email = "mishkov@misho.com",
                            FirstName = "Misho",
                            IsDeleted = false,
                            LastName = "Mishkov"
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 2,
                            Email = "petio@mvc.net",
                            FirstName = "Peter",
                            IsDeleted = false,
                            LastName = "Petrov"
                        },
                        new
                        {
                            Id = 3,
                            AddressId = 3,
                            Email = "koksal@asd.tr",
                            FirstName = "Koksal",
                            IsDeleted = false,
                            LastName = "Baba"
                        },
                        new
                        {
                            Id = 4,
                            AddressId = 4,
                            Email = "indebt@greece.gov",
                            FirstName = "Nikolaos",
                            IsDeleted = false,
                            LastName = "Tsitsibaris"
                        });
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex(new[] { "AddressId" }, "IX_Employees_AddressId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Email = "djoro@ekont.com",
                            FirstName = "Djoro",
                            IsDeleted = false,
                            LastName = "Emploev"
                        },
                        new
                        {
                            Id = 2,
                            Email = "gonzales@speedy.net",
                            FirstName = "Speedy",
                            IsDeleted = false,
                            LastName = "Gonzales"
                        },
                        new
                        {
                            Id = 3,
                            Email = "dormut@dhl.tr",
                            FirstName = "Dormut",
                            IsDeleted = false,
                            LastName = "Baba"
                        },
                        new
                        {
                            Id = 4,
                            Email = "ontime@fedex.us",
                            FirstName = "Stafanakis",
                            IsDeleted = false,
                            LastName = "Kurierakis"
                        });
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

                    b.Property<bool>("IsDeleted")
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CustomerId = 1,
                            DeliverToAddress = true,
                            IsDeleted = false,
                            ShipmentId = 1,
                            WareHouseId = 1,
                            Weight = 1234.5599999999999
                        });
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OriginWareHouseId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DestinationWareHouseId" }, "IX_Shipments_DestinationWareHouseId");

                    b.HasIndex(new[] { "OriginWareHouseId" }, "IX_Shipments_OriginWareHouseId");

                    b.HasIndex(new[] { "StatusId" }, "IX_Shipments_StatusId");

                    b.ToTable("Shipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArrivalDate = new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            DepartureDate = new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            DestinationWareHouseId = 2,
                            IsDeleted = false,
                            OriginWareHouseId = 1,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            ArrivalDate = new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local),
                            DepartureDate = new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            DestinationWareHouseId = 2,
                            IsDeleted = false,
                            OriginWareHouseId = 1,
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Preparing"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "On the way"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("DeliverIT.Models.DatabaseModels.WareHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AddressId" }, "IX_WareHouses_AddressId")
                        .IsUnique();

                    b.ToTable("WareHouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 2,
                            IsDeleted = false
                        });
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