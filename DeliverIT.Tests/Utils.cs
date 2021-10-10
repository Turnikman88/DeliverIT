using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Tests
{
    public class Utils
    {
        public static DbContextOptions<DeliverITDBContext> GetOptions(string dbName)
        {
            return new DbContextOptionsBuilder<DeliverITDBContext>().UseInMemoryDatabase(dbName).Options;
        }

        public static List<Country> GetCountries()
        {
            return new List<Country> { new Country
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
        }

        public static List<City> GetCities()
        {


            return new List<City>()
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
                    Name = "Plovdiv",
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
        }
        /*var addresses = new List<Address>()
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
        var roles = new List<AppRole>()
        {
            new AppRole
            {
                Id = 1,
                Name = "Admin",
            },
            new AppRole
            {
                Id = 2,
                Name = "User",
            }
        };
        var customers = new List<Customer>()
        {
            new Customer
            {
                Id = 1,
                FirstName = "Misho",
                LastName = "Mishkov",
                Email = "mishkov@misho.com",
                Password = "12345678",
                AddressId = 1
            },
            new Customer
            {
                Id = 2,
                FirstName = "Peter",
                LastName = "Petrov",
                Email = "petio@mvc.net",
                Password = "123456789",
                AddressId = 2
            },
            new Customer
            {
                Id = 3,
                FirstName = "Koksal",
                LastName = "Baba",
                Email = "koksal@asd.tr",
                Password = "1234567899",
                AddressId = 3
            },
            new Customer
            {
                Id = 4,
                FirstName = "Nikolaos",
                LastName = "Tsitsibaris",
                Email = "indebt@greece.gov",
                Password = "12345678999",
                AddressId = 4
            }
        };
        var employees = new List<Employee>()
        {
            new Employee
            {
                Id = 5,
                FirstName = "Djoro",
                LastName = "Emploev",
                Email = "djoro@ekont.com",
                Password = "adminadmin",
                AddressId = 1
            },
            new Employee
            {
                Id = 6,
                FirstName = "Speedy",
                LastName = "Gonzales",
                Email = "gonzales@speedy.net",
                Password = "adminadmin1",
                AddressId = null
            },
            new Employee
            {
                Id = 7,
                FirstName = "Dormut",
                LastName = "Baba",
                Email = "dormut@dhl.tr",
                Password = "adminadmin2",
                AddressId = null
            },
            new Employee
            {
                Id = 8,
                FirstName = "Stafanakis",
                LastName = "Kurierakis",
                Email = "ontime@fedex.us",
                Password = "adminadmin3",
                AddressId = null
            }
        };
        var userRoles = new List<AppUserRole>()
        {
            new AppUserRole()
            {
                AppRoleId = 2,
                AppUserId = customers[0].Id
            },
            new AppUserRole()
            {
                AppRoleId = 2,    // Customers
                AppUserId = customers[1].Id
            },
            new AppUserRole()
            {
                AppRoleId = 2,    // Customers
                AppUserId = customers[2].Id
            },
            new AppUserRole()
            {
                AppRoleId = 2,    // Customers
                AppUserId = customers[3].Id
            },
            new AppUserRole()
            {
                AppRoleId = 1,    // Employees/Admin
                AppUserId = employees[0].Id
            },
            new AppUserRole()
            {
                AppRoleId = 1,    // Employees/Admin
                AppUserId = employees[1].Id
            },
            new AppUserRole()
            {
                AppRoleId = 1,    // Employees/Admin
                AppUserId = employees[2].Id
            },
            new AppUserRole()
            {
                AppRoleId = 1,    // Employees/Admin
                AppUserId = employees[3].Id
            }
        };
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
            };*/
        
    }    
}