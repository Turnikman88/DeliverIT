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
    public class CountryService : ICountryService
    {
        private readonly DeliverITDBContext _db;

        public CountryService(DeliverITDBContext db)
        {
            this._db = db;
        }
        
        public async Task<CountryDTO> GetCountryByIdAsync(int id)
        {
            return CountryDTOMapperExtension.GetDTO(await _db.Countries
                .Where(x => x.Id == id).Include(c=>c.Cities).FirstOrDefaultAsync()) ?? throw new AppException(Constants.COUNTRY_NOT_FOUND);
        }

        public async Task<CountryDTO> GetCountryByNameAsync(string name)
        {
            return CountryDTOMapperExtension.GetDTO(await _db.Countries.Include(c => c.Cities)
                .Where(x => x.Name == name).FirstOrDefaultAsync()) ?? throw new AppException(Constants.COUNTRY_NOT_FOUND);
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesByPartNameAsync(string part)
        {
            return await _db.Countries.Where(x => x.Name.Contains(part)).Include(c => c.Cities).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<IEnumerable<CountryDTO>> GetAsync()
        {
            return await _db.Countries.Include(c => c.Cities).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CountryDTO> PostAsync(CountryDTO obj)
        {
            _ = await _db.Countries.FirstOrDefaultAsync(x => x.Name == obj.Name) != null 
                ? throw new AppException(Constants.COUNTRY_EXISTS) : 0;

            var newCountry = obj.GetEntity();
            var deletedCountry = await _db.Countries.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == obj.Name && x.IsDeleted == true);

            if (deletedCountry == null)
            {
                await this._db.Countries.AddAsync(newCountry);
                await _db.SaveChangesAsync();
                obj.Id = newCountry.Id;
            }
            else
            {
                deletedCountry.DeletedOn = null;
                deletedCountry.IsDeleted = false;
                await _db.SaveChangesAsync();
                obj.Id = deletedCountry.Id;
            }
            
            return obj;
        }

        public async Task<CountryDTO> UpdateAsync(int id, CountryDTO obj)
        {
            _ = await _db.Countries.FirstOrDefaultAsync(x => x.Name == obj.Name) != null ? throw new AppException(Constants.COUNTRY_EXISTS) : 0;

            if (string.IsNullOrEmpty(obj.Name))
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            var model = await this._db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new AppException(Constants.COUNTRY_NOT_FOUND);

            model.Name = obj.Name;
            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<CountryDTO> DeleteAsync(int id)
        {
            var model = await this._db.Countries.Include(c => c.Cities).FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new AppException(Constants.COUNTRY_NOT_FOUND);

            model.DeletedOn = System.DateTime.Now;
            this._db.Countries.Remove(model);
            await _db.SaveChangesAsync();

            return CountryDTOMapperExtension.GetDTO(model);
        }
    }
}
