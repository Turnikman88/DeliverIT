using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IAddressService
    {
        Task<int> AddressToID(string address, string city, string country);
    }
}
