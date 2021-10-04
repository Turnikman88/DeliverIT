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
        Task<IEnumerable<ParcelDTO>> FilterByWeightAsync(string criteria, double weight);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerIdAsync(int id);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerNameAsync(string name);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerEmailAsync(string email);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerAddressAsync(string address);
        Task<IEnumerable<ParcelDTO>> FilterByWareHouseAsyncId(int id);
        Task<IEnumerable<ParcelDTO>> FilterByCategoryIdAsync(int id);
        Task<IEnumerable<ParcelDTO>> FilterByCategoryNameAsync(string name);
        Task<IEnumerable<ParcelDTO>> FilterByCustomerAndCategoryIdAsync(int customerd, int categoryId); // ToDo: maybe more filters?
        Task<IEnumerable<ParcelDTO>> SortByWeightAsync();
        Task<IEnumerable<ParcelDTO>> SortByArrivalDateAsync();
        Task<IEnumerable<ParcelDTO>> SortByWeightAndArrivalDateAsync();

    }
}
