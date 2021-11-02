using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICountryService : ICRUDshared<CountryDTO>
    {
        Task<CountryDTO> GetCountryByIdAsync(int id);
        Task<CountryDTO> GetCountryByNameAsync(string name);
        Task<IEnumerable<CountryDTO>> GetCountriesByPartNameAsync(string part);
        Task<bool> CountryExists(string name);
    }
}
