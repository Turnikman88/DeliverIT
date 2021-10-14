using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IParcelService : ICRUDshared<ParcelDTO>
    {
        Task<IEnumerable<ParcelDTO>> ListCustomerIncomingParcelsAsync(int id);
        Task<ParcelDTO> GetParcelByIdAsync(int id);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerIdAsync(int id);
        Task<IEnumerable<ParcelDTO>> MultiFilterAsync(int? id, int? customerId, int? shipmentId,
                    int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight);
        Task<IEnumerable<string>> GetShipmentStatusAsync(int customerId);
        Task<IEnumerable<ParcelDTO>> SortByWeightAsync();
        Task<IEnumerable<ParcelDTO>> SortByArrivalDateAsync();
        Task<IEnumerable<ParcelDTO>> SortByWeightAndArrivalDateAsync();
        Task<ParcelDTO> ChangeDeliverLocationAsync(int id);
    }
}
