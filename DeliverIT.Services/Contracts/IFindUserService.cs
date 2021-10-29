using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IFindUserService
    {
        Task<bool> IsExistingAsync(string email);
        Task<UserDTO> FindUserAsync(string authorization);
        Task<EmployeeDTO> FindEmployee(string authorization);
    }
}
