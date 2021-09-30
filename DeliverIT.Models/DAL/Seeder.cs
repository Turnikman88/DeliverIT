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
        }
    }
}

