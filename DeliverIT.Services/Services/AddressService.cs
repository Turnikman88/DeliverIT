using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly DeliverITDBContext _db;

        public AddressService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public async Task<int> AddressToID(string address, string city, string country)
        {
            var validAddress = await this._db.Addresses
                                         .Include(x => x.City).ThenInclude(x => x.Country)
                                         .FirstOrDefaultAsync(x => x.StreetName == address
                                         && x.City.Name == city
                                         && x.City.Country.Name == country);

            if (validAddress is null)
            {
                Country countryobj = await this._db.Countries
                                                .FirstOrDefaultAsync(x => x.Name == country);
                City cityobj = await this._db.Cities
                                                .FirstOrDefaultAsync(x => x.Name == city);
                Address addressobj = null;

                if (countryobj is null)
                {
                    countryobj = new Country
                    {
                        Name = country
                    };

                    await this._db.Countries.AddAsync(countryobj);
                    await this._db.SaveChangesAsync();
                }

                if (cityobj is null)
                {
                    cityobj = new City
                    {
                        Name = city,
                        CountryId = countryobj.Id
                    };

                    await this._db.Cities.AddAsync(cityobj);
                    await this._db.SaveChangesAsync();

                }

                addressobj = new Address
                {
                    StreetName = address,
                    CityId = cityobj.Id
                };

                await this._db.Addresses.AddAsync(addressobj);
                await this._db.SaveChangesAsync();

                return addressobj.Id;
            }

            return validAddress.Id;
        }
    }
}
