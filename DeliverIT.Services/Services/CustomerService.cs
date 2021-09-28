using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using System.Linq;


namespace DeliverIT.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverITDBContext db;

        public CustomerService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public int UserCount()
        {
            return this.db.Customers.Count();
        }
    }
}
