using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IWareHouseService : ICRUDshared<WareHouseDTO>
    {
        Task<WareHouseDTO> GetWareHouseByIdAsync(int id);
        Task<IEnumerable<string>> GetAddressesAsync();
        IQueryable<Address> GetAddressesObject();
    }
}
