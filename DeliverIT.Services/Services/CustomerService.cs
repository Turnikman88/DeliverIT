using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverITDBContext db;

        public CustomerService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<CustomerDTO> DeleteAsync(int id)
        {
            var customer = await db.Customers.Include(x => x.AddressId).FirstOrDefaultAsync(x => x.Id == id);
            var customerDTO = customer.GetDTO();

            customer.DeletedOn = DateTime.Now;
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return customerDTO;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAsync()
        {
            return await db.Customers.Include(x => x.AddressId).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CustomerDTO> PostAsync(CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDTO> UpdateAsync(int id, CustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task UserCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
