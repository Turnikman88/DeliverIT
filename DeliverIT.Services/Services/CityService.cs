using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class CityService : ICityService
    {
        private readonly DeliverITDBContext _db;

        public CityService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<CityDTO>> GetAsync()
        {
            return await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .Select(x => new CityDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    CountryId = x.CountryId,
                    CountryName = x.Country.Name,
                    Addresses = x.Addresses.Select(y => y.StreetName).ToList()
                }).ToListAsync(); //JSON does not return arr of countries and addresses
        }

        public async Task<CityDTO> GetCityByIdAsync(int id)
        {
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
            return city.GetDTO();
        }

        public async Task<CityDTO> GetCityByNameAsync(string name)
        {
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return city.GetDTO();
        }

        public async Task<CityDTO> PostAsync(CityDTO obj)
        {
            CityDTO result = null;
            var deletedCity = await _db.Cities.Include(x => x.Country).IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.CountryId == obj.CountryId && x.Name == obj.Name && x.IsDeleted == true);
            var newCity = obj.GetEntity();
            if (deletedCity == null)
            {
                await this._db.Cities.AddAsync(newCity);
                await _db.SaveChangesAsync();
                newCity = await _db.Cities.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == newCity.Id);
                result = newCity.GetDTO();
            }
            else
            {
                deletedCity.DeletedOn = null;
                deletedCity.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deletedCity.GetDTO();
            }

            return result;
        }

        public async Task<CityDTO> UpdateAsync(int id, CityDTO obj)
        {
            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            city.Name = obj.Name;
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }
        public async Task<CityDTO> DeleteAsync(int id)
        {
            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            city.DeletedOn = System.DateTime.Now;
            this._db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }
    }
}
