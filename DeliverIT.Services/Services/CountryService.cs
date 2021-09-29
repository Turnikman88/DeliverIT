using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Country> GetCountryById(int id)
        {
            return await db.Countries.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Country> GetCountryByName(string name)
        {            
            return await db.Countries.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Country>> GetCountriesByPartName(string part)
        {
            return await db.Countries.Where(x => x.Name.Contains(part)).ToListAsync();
        }

        public async Task<IEnumerable<Country>> Get()
        {
            return await this.db.Countries.ToListAsync();
        }

        public async Task<Country> Post(Country obj)
        {
            await this.db.Countries.AddAsync(obj);
            await db.SaveChangesAsync();

            return obj;
        }

        public async Task<Country> Update(int id, Country obj)
        {
            var model = await this.db.Countries.FindAsync(id);            

            model.Name = obj.Name;
            await db.SaveChangesAsync();

            return model;
        }

        public async Task<Country> Delete(int id)
        {
            var model = await this.db.Countries.FindAsync(id);            

            this.db.Countries.Remove(model);
            await db.SaveChangesAsync();

            return model;
        }

    }
}
