using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IAddressService
    {
        Task<int> AddressToID(string address, string city, string country);
        Task<IEnumerable<CountryDTO>> GetCountries();
        Task<IEnumerable<CityDTO>> GetCities(string countryName);
    }
}
