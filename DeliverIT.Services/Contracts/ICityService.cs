using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICityService : ICRUDshared<CityDTO>
    {
        Task<CityDTO> GetCityByIdAsync(int id);
        Task<CityDTO> GetCityByNameAsync(string name);
        Task<bool> CityExists(string name, int countryId);
        Task<IEnumerable<CityDTO>> GetCitiesByNameAsync(string name);
        Task<IEnumerable<CityDTO>> GetCitiesByCountryNameAsync(string name);
    }
}
