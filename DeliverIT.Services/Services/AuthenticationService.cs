using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using System.Linq;

namespace DeliverIT.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DeliverITDBContext db;
        public AuthenticationService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public bool FindUser(string email)
        {

            return this.db.Customers.Any(x => x.Email == email);
        }

        public bool FindEmployee(string email) //TODO: await?
        {
            return this.db.Employees.Any(x => x.Email == email);
        }
    }
}
