using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IAuthenticationService
    {
        bool FindUser(string email);
        bool FindEmployee(string email);
        Task<string> GetUserID(string credentials);
        Task<string> GetEmployeeID(string credentials);
    }
}
