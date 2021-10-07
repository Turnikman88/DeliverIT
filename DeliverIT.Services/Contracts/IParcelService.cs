using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IParcelService : ICRUDshared<ParcelDTO>
    {
        Task<ParcelDTO> GetParcelByIdAsync(int id);
        Task<bool> ParcelExistsAsync(int id);        
        Task<IEnumerable<ParcelDTO>> MultiFilterAsync(int? id, int? customerId, int? shipmentId,
                    int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight);
        Task<IEnumerable<ParcelDTO>> SortByWeightAsync();
        Task<IEnumerable<ParcelDTO>> SortByArrivalDateAsync();
        Task<IEnumerable<ParcelDTO>> SortByWeightAndArrivalDateAsync();

    }
}
