using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICountryService : ICRUDshared<CountryDTO>
    {
        Task<CountryDTO> GetCountryById(int id);
        Task<CountryDTO> GetCountryByName(string name);
        Task<IEnumerable<CountryDTO>> GetCountriesByPartName(string part);
    }
}
