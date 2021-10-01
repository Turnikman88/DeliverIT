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
        private readonly DeliverITDBContext db;

        public CityService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CityDTO>> GetAsync()
        {
            return await this.db.Cities
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
            var city = await db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)                
                .FirstOrDefaultAsync(x => x.Id == id);
            return city.GetDTO();
        }

        public async Task<CityDTO> GetCityByNameAsync(string name)
        {
            var city = await db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return city.GetDTO();
        }

        public async Task<CityDTO> PostAsync(CityDTO obj)
        {
            var deletedCity = await db.Cities.Include(x => x.Country).IgnoreQueryFilters().FirstOrDefaultAsync(x => x.CountryId == obj.CountryId && x.Name == obj.Name);
            var newCity = obj.GetEntity();
            if (deletedCity == null)
            {
                await this.db.Cities.AddAsync(newCity);
                await db.SaveChangesAsync();
                obj.Id = newCity.Id;
                obj.CountryName = await db.Countries.Where(x => x.Id == newCity.CountryId).Select(x => x.Name).FirstOrDefaultAsync();
            }
            else
            {
                deletedCity.IsDeleted = false;
                await db.SaveChangesAsync();
                obj.Id = deletedCity.Id;
                obj.CountryName = deletedCity.Country.Name;
            }

            return obj;
        }

        public async Task<CityDTO> UpdateAsync(int id, CityDTO obj)
        {
            var city = await this.db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            city.Name = obj.Name;
            await db.SaveChangesAsync();

            return city.GetDTO();
        }
        public async Task<CityDTO> DeleteAsync(int id)
        {
            var city = await this.db.Cities
                .Include(x => x.Addresses)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            this.db.Cities.Remove(city);
            await db.SaveChangesAsync();

            return city.GetDTO();
        }
    }
}
