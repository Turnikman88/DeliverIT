using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IFindUserService
    {
        Task<bool> IsExisting(string email);
        Task<UserDTO> FindUs(string authorization);
    }
}
