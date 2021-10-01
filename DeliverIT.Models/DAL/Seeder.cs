using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DeliverIT.Models.DAL
{
    public static class Seeder
    {
        public static void Seed(this ModelBuilder db)
        {
            var countries = new List<Country>()
            {
             new Country
                {
                    Id = 1,
                    Name = "Bulgaria"
                },
             new Country
                {
                    Id = 2,
                    Name = "Turkey"
                },
             new Country
                {
                    Id = 3,
                    Name = "Greece"
                },
              new Country
                {
                    Id = 4,
                    Name = "Romania"
                }
            };

            db.Entity<Country>().HasData(countries);

            var cities = new List<City>()
            {
                new City
                {
                    Id = 1,
                    Name = "Sofia",
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Plovidv",
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Istanbul",
                    CountryId = 2
                },
                new City
                {
                    Id = 4,
                    Name = "Athenes",
                    CountryId = 3
                },
                new City
                {
                    Id = 5,
                    Name = "Yash",
                    CountryId = 4
                }
            };

            db.Entity<City>().HasData(cities);

            var addresses = new List<Address>()
            {
                new Address
                {
                    Id = 1,
                    CityId = 1,
                    StreetName = "Vasil Levski 14"
                },
                new Address
                {
                    Id = 2,
                    CityId = 2,
                    StreetName = "blv. Iztochen 23"
                },
                new Address
                {
                    Id = 3,
                    CityId = 3,
                    StreetName = "blv. Halic 12"
                },
                new Address
                {
                    Id = 4,
                    CityId = 4,
                    StreetName = "blv. Zeus 12"
                },
                new Address
                {
                    Id = 5,
                    CityId = 5,
                    StreetName = "blv. Romunska Morava 1"
                }
            };

            db.Entity<Address>().HasData(addresses);

            var statuses = new List<Status>()
            {
                new Status
                {
                    Id = 1,
                    Name = "Preparing"
                },
                new Status
                {
                    Id = 2,
                    Name = "On the way"
                },
                new Status
                {
                    Id = 3,
                    Name = "Completed"
                }
            };

            db.Entity<Status>().HasData(statuses);

            var categories = new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    Name = "Electronics"
                },
                new Category
                {
                    Id = 2,
                    Name = "Shoes"
                },
                new Category
                {
                    Id = 3,
                    Name = "Clothing"
                },
                new Category
                {
                    Id = 4,
                    Name = "Medical supplies"
                }
            };

            db.Entity<Category>().HasData(categories);

            var customers = new List<Customer>() 
            { 
                new Customer
                {
                    Id = 1,
                    FirstName = "Misho",
                    LastName = "Mishkov",
                    Email = "mishkov@misho.com",
                    AddressId = 1
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Peter",
                    LastName = "Petrov",
                    Email = "petio@mvc.net",
                    AddressId = 2
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Koksal",
                    LastName = "Baba",
                    Email = "koksal@asd.tr",
                    AddressId = 3
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "Nikolaos",
                    LastName = "Tsitsibaris",
                    Email = "indebt@greece.gov",
                    AddressId = 4
                }
            };

            db.Entity<Customer>().HasData(customers);

            var employees = new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "Djoro",
                    LastName = "Emploev",
                    Email = "djoro@ekont.com",
                    AddressId = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Speedy",
                    LastName = "Gonzales",
                    Email = "gonzales@speedy.net",
                    AddressId = null
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Dormut",
                    LastName = "Baba",
                    Email = "dormut@dhl.tr",
                    AddressId = null
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Stafanakis",
                    LastName = "Kurierakis",
                    Email = "ontime@fedex.us",
                    AddressId = null
                }
            };

            db.Entity<Employee>().HasData(employees);

            var warehouses = new List<WareHouse>() 
            {
                new WareHouse
                {
                    Id = 1,
                    AddressId = 1
                },
                new WareHouse
                {
                    Id = 2,
                    AddressId = 2
                }
            };

            db.Entity<WareHouse>().HasData(warehouses);

            var shipments = new List<Shipment>()
            {
                new Shipment
                {
                    Id = 1,
                    DepartureDate = System.DateTime.Today.AddDays(5),
                    ArrivalDate = System.DateTime.Today.AddDays(10),
                    OriginWareHouseId = 1,
                    DestinationWareHouseId = 2,
                    StatusId = 1
                },
                new Shipment
                {
                    Id = 2,
                    DepartureDate = System.DateTime.Today.AddDays(5),
                    ArrivalDate = System.DateTime.Today.AddDays(10),
                    OriginWareHouseId = 1,
                    DestinationWareHouseId = 2,
                    StatusId = 1
                }
            };

            db.Entity<Shipment>().HasData(shipments);

            var parcels = new List<Parcel>()
             {
                new Parcel
                {
                    Id = 1,
                    CustomerId = 1,
                    ShipmentId = 1,
                    WareHouseId = 1,
                    CategoryId = 1,
                    Weight = 1234.56,
                    DeliverToAddress = true
                }
             };

            db.Entity<Parcel>().HasData(parcels);
        }
    }
}

