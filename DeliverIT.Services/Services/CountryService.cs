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
    public class CountryService : ICountryService
    {
        private readonly DeliverITDBContext db;

        public CountryService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<CountryDTO> GetCountryById(int id)
        {
            return CountryDTOMapperExtension.GetDTO(await db.Countries.Where(x => x.Id == id).FirstOrDefaultAsync());
        }

        public async Task<CountryDTO> GetCountryByName(string name)
        {
            return CountryDTOMapperExtension.GetDTO(await db.Countries.Where(x => x.Name == name).FirstOrDefaultAsync());
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesByPartName(string part)
        {
            return await db.Countries.Where(x => x.Name.Contains(part)).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<CountryDTO>> Get()
        {
            return await db.Countries.Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CountryDTO> Post(CountryDTO obj)
        {
            var newCountry = obj.GetEntity();

            await this.db.Countries.AddAsync(newCountry);
            await db.SaveChangesAsync();
            obj.Id = newCountry.Id;
            //var newCountry = obj.GetDTO();
            //return newCountry;
            return obj;
        }

        public async Task<CountryDTO> Update(int id, CountryDTO obj)
        {
            var model = await this.db.Countries.FindAsync(id);

            model.Name = obj.Name;
            await db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<CountryDTO> Delete(int id)
        {
            var model = await this.db.Countries.FindAsync(id);

            this.db.Countries.Remove(model);
            await db.SaveChangesAsync();

            return CountryDTOMapperExtension.GetDTO(model);
        }

    }
}
