using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICityService : ICRUDshared<CityDTO>
    {
        Task<CityDTO> GetCityById(int id);
        Task<CityDTO> GetCityByName(string name);
    }
}
