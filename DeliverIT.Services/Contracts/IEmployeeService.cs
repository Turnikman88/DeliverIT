using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IEmployeeService : ICRUDshared<EmployeeDTO>
    {
        Task<EmployeeDTO> GetEmployeeByEmail(string email);
    }
}
