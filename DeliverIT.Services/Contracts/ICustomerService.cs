using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface ICustomerService : ICRUDshared<CustomerDTO>
    {
        Task UserCountAsync();
    }
}
