using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IShipmentService : ICRUDshared<ShipmentDTO>
    {
        Task<bool> ShipmentExistsAsync(int id);
        Task<ShipmentDTO> GetShipmentByIdAsync(int id);
        Task<IEnumerable<ShipmentDTO>> FilterByDestinationWareHouseAsync(int id);
        Task<IEnumerable<ShipmentDTO>> FilterByOriginWareHouseAsync(int id);
        Task<IEnumerable<ShipmentDTO>> FilterByCustomerIdAsync(int id);
        Task<IEnumerable<ShipmentDTO>> FilterByCustomerNameAsync(string name);
        Task<IEnumerable<ShipmentDTO>> FilterByCustomerEmailAsync(string email);
        Task<IEnumerable<ShipmentDTO>> FilterByCustomerAddressAsync(string address);
        Task<IEnumerable<ShipmentDTO>> FilterByStatusIdAsync(int id);
    }
}
