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

        public bool FindUser(string authorization)
        {
            if (authorization == null || !authorization.Contains(" "))
            {
                return false;
            }
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            return this.db.Customers.Any(x => x.Email == email && x.Password == password);
        }

        public bool FindEmployee(string authorization) //TODO: await?
        {
            if (authorization == null || !authorization.Contains(" "))
            {
                return false;
            }
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            return this.db.Employees.Any(x => x.Email == email && x.Password == password);
        }
    }
}
