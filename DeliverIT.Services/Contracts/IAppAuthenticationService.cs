using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IFindUserService
    {
        Task<UserDTO> FindUs(string authorization);
    }
}
