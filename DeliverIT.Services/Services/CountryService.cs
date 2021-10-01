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
        
        public async Task<CountryDTO> GetCountryByIdAsync(int id)
        {
            return CountryDTOMapperExtension.GetDTO(await db.Countries.Where(x => x.Id == id).Include(c=>c.Cities).FirstOrDefaultAsync());
        }

        public async Task<CountryDTO> GetCountryByNameAsync(string name)
        {
            return CountryDTOMapperExtension.GetDTO(await db.Countries.Include(c => c.Cities).Where(x => x.Name == name).FirstOrDefaultAsync());
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesByPartNameAsync(string part)
        {
            return await db.Countries.Where(x => x.Name.Contains(part)).Include(c => c.Cities).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<CountryDTO>> GetAsync()
        {
            return await db.Countries.Include(c => c.Cities).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CountryDTO> PostAsync(CountryDTO obj)
        {
            var newCountry = obj.GetEntity();
            var deletedCountry = await db.Countries.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == obj.Name && x.IsDeleted == true);

            if (deletedCountry == null)
            {
                await this.db.Countries.AddAsync(newCountry);
                await db.SaveChangesAsync();
                obj.Id = newCountry.Id;
            }
            else
            {
                deletedCountry.DeletedOn = null;
                deletedCountry.IsDeleted = false;
                await db.SaveChangesAsync();
                obj.Id = deletedCountry.Id;
            }
            
            return obj;
        }

        public async Task<CountryDTO> UpdateAsync(int id, CountryDTO obj)
        {
            var model = await this.db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id);

            model.Name = obj.Name;
            await db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<CountryDTO> DeleteAsync(int id)
        {
            var model = await this.db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id);
            model.DeletedOn = System.DateTime.Now;
            this.db.Countries.Remove(model);
            await db.SaveChangesAsync();

            return CountryDTOMapperExtension.GetDTO(model);
        }
    }
}
