using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICityService : ICRUDshared<CityDTO>
    {
        Task<CityDTO> GetCityByIdAsync(int id);
        Task<CityDTO> GetCityByNameAsync(string name);
    }
}
