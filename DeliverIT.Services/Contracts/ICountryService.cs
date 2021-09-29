using DeliverIT.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICountryService : ICRUDshared<Country>
    {        
        Task<Country> GetCountryById(int id);
        Task<Country> GetCountryByName(string name);
        Task<IEnumerable<Country>> GetCountriesByPartName(string part);
    }
}
