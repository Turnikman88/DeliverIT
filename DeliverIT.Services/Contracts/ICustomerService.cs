﻿using DeliverIT.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICustomerService : ICRUDshared<CustomerDTO>
    {
        Task<int> UserCountAsync();
        Task<IEnumerable<CustomerDTO>> GetCustomerByNameAsync(string name);
        Task<CustomerDTO> GetCustomerByIDAsync(int id);
        Task<IEnumerable<CustomerDTO>> GetCustomersByEmailAsync(string part);
    }
}
