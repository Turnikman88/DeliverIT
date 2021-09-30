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

        public async Task<IEnumerable<CityDTO>> Get()
        {
            return await this.db.Cities
                .Include(x => x.Country)
                .Select(x => new CityDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    CountryId = x.CountryId,
                    CountryName = x.Country.Name,
                }).ToListAsync(); //JSON does not return arr of countries and addresses
        }

        public async Task<CityDTO> GetCityById(int id)
        {
            var city = await db.Cities
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
            return city.GetDTO();
        }

        public async Task<CityDTO> GetCityByName(string name)
        {
            var city = await db.Cities
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return city.GetDTO();
        }

        public async Task<CityDTO> Post(CityDTO obj)
        {
            var newCity = obj.GetEntity();
            await this.db.Cities.AddAsync(newCity);
            await db.SaveChangesAsync();

            var result = db.Cities.Include(x => x.Country);
            var city = result.FirstOrDefault(x => x.Name == obj.Name);
            obj.Id = city.Id;

            return obj;
        }

        public async Task<CityDTO> Update(int id, CityDTO obj)
        {
            var city = await this.db.Cities.FindAsync(id);

            city.Name = obj.Name;
            await db.SaveChangesAsync();

            return city.GetDTO();
        }
        public async Task<CityDTO> Delete(int id)
        {
            var city = await this.db.Cities.FindAsync(id);

            this.db.Cities.Remove(city);
            await db.SaveChangesAsync();

            return city.GetDTO();
        }
    }
}
