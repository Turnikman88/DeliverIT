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
                    Id = 1,
                    Name = "Medical supplies"
                }
            };

            db.Entity<Category>().HasData(categories);


        }
    }
}

