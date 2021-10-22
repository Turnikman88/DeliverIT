using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IAddressService
    {
        Task<int> AddressToID(string address, string city, string country);
    }
}
