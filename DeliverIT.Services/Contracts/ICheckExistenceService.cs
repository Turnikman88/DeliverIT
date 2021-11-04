using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICheckExistenceService
    {
        Task<bool> CityExists(string name, int countryId);
        Task<bool> CountryExists(string name);
        Task<bool> CustomerExists(int id);
        Task<bool> ShipmentExists(int id);
        Task<bool> WarehouseExists(int? id);
    }
}