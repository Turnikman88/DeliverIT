using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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
                }).ToListAsync();
        }

        public async Task<CityDTO> GetCityByIdAsync(int id)
        {
            CheckId(id);
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CITY_NOT_FOUND);

            return city.GetDTO();
        }


        public async Task<CityDTO> GetCityByNameAsync(string name)
        {
            var city = await _db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower())
                ?? throw new AppException(Constants.CITY_NOT_FOUND);

            return city.GetDTO();
        }



        public async Task<CityDTO> PostAsync(CityDTO obj)
        {
            //_ = CityExists(obj.Name, obj.CountryId)
            //    != null ? throw new AppException(Constants.CITY_EXISTS) : 0;

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
            _ = CityExists(obj.Name, obj.CountryId)
                != null ? throw new AppException(Constants.CITY_EXISTS) : 0;
            CheckId(id);

            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CITY_NOT_FOUND);

            if (obj.Name == null)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            city.Name = obj.Name;
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }

        public async Task<CityDTO> DeleteAsync(int id)
        {
            CheckId(id);

            var city = await this._db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CITY_NOT_FOUND);

            city.DeletedOn = System.DateTime.Now;
            this._db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return city.GetDTO();
        }

        public async Task<bool> CityExists(string name, int countryId)
        {
            return await _db.Cities.AnyAsync(x => x.Name == name && x.CountryId == countryId);
        }

        private static void CheckId(int id)
        {
            if (id <= 0)
            {
                throw new AppException(Constants.INVALID_ID);
            }
        }
    }
}
