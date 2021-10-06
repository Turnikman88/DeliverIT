namespace DeliverIT.Services.Contracts
{
    public interface IAuthenticationService
    {
        bool FindUser(string email);
        bool FindEmployee(string email);
    }
}
