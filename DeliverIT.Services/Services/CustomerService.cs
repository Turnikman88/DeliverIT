using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverITDBContext _db;

        public CustomerService(DeliverITDBContext db)
        {
            this._db = db;
        }
        public async Task<CustomerDTO> DeleteAsync(int id)
        {
            var customer = await _db.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CUSTOMER_NOT_FOUND);

            var customerDTO = customer.GetDTO();

            customer.DeletedOn = DateTime.Now;
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();

            return customerDTO;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAsync()
        {
            return await _db.Customers.Include(x => x.Address)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomerByNameAsync(string name)
        {
            return await _db.Customers.Include(x => x.Parcels)
                .ThenInclude(x => x.Category)
                .Include(x => x.Parcels).ThenInclude(x => x.Shipment).ThenInclude(x => x.Status)
                .Include(x => x.Address)
                .Where(x => x.FirstName.ToLower() == name.ToLower() || x.LastName.ToLower() == name.ToLower())
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<CustomerDTO> PostAsync(CustomerDTO obj)
        {
            _ = await _db.Customers.FirstOrDefaultAsync(x => x.Email == obj.Email && x.IsDeleted == false) != null ? throw new AppException(Constants.CUSTOMER_EXISTS) : 0;

            CustomerDTO result = null;
            var newCustomer = obj.GetEntity();

            var deletedCustomer = await _db.Customers.Include(x => x.Parcels)
                .ThenInclude(x => x.Category)
                .Include(x => x.Parcels).ThenInclude(x => x.Shipment).ThenInclude(x => x.Status)
                .Include(x => x.Address).IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Email == obj.Email && x.IsDeleted == true);
            if (deletedCustomer == null)
            {
                await _db.Customers.AddAsync(newCustomer);
                await this._db.SaveChangesAsync();

                newCustomer = await this._db.Customers.Include(x => x.Parcels)
                .ThenInclude(x => x.Category)
                .Include(x => x.Parcels).ThenInclude(x => x.Shipment).ThenInclude(x => x.Status)
                .Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == newCustomer.Id);
                result = newCustomer.GetDTO();
            }
            else
            {
                deletedCustomer.DeletedOn = null;
                deletedCustomer.IsDeleted = false;
                await this._db.SaveChangesAsync();
                result = deletedCustomer.GetDTO();
            }

            return result;
        }

        public async Task<CustomerDTO> UpdateAsync(int id, CustomerDTO obj)
        {
            _ = await _db.Customers.FirstOrDefaultAsync(x => x.Email == obj.Email) != null ? throw new AppException(Constants.CUSTOMER_EXISTS) : 0;

            var model = await _db.Customers.Include(c => c.Address).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CUSTOMER_NOT_FOUND);

            if (obj.FirstName == null || obj.LastName == null || obj.AddressId <= 0)
            {
                throw new AppException(Constants.CUSTOMER_NOT_FOUND);
            }

            model.FirstName = obj.FirstName;
            model.LastName = obj.LastName;
            model.AddressId = obj.AddressId;
            model.Email = obj.Email;
            model.Password = obj.Password;

            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        public async Task<int> UserCountAsync()
        {
            return await _db.Customers.CountAsync();
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersByEmailAsync(string part)
        {
            return await _db.Customers.Where(x => x.Email.Contains(part)).Include(c => c.Address).Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CustomerDTO> GetCustomerByIDAsync(int id)
        {
            return CustomerDTOMapperExtension.GetDTO(await _db.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id))
                ?? throw new AppException(Constants.CUSTOMER_NOT_FOUND);
        }
    }
}
