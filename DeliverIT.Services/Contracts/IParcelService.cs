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
    }
}
