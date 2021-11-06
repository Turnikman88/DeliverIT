using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly DeliverITDBContext _db;
        private readonly ICountryService _cs;
        private readonly ICityService _ss;

        public AddressService(DeliverITDBContext db, ICountryService cs, ICityService ss)
        {
            this._db = db;
            this._cs = cs;
            this._ss = ss;
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

        public async Task<IEnumerable<CountryDTO>> GetCountries()
        {
            return await _cs.GetAsync();
        }
        public async Task<IEnumerable<CityDTO>> GetCities(string countryName)
        {
            return await _ss.GetCitiesByCountryNameAsync(countryName);
        }
    }
}
