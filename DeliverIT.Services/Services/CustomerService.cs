using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<IEnumerable<CustomerDTO>> GetCustomersByNameAsync(string name)
        {
            return await _db.Customers.Include(x => x.Parcels).ThenInclude(x => x.Category)
                                       .Include(x => x.Parcels).ThenInclude(x => x.Shipment).ThenInclude(x => x.Status)
                                       .Include(x => x.Address)
                                       .Where(x => x.FirstName.ToLower() == name.ToLower() || x.LastName.ToLower() == name.ToLower())
                                       .Select(x => x.GetDTO())
                                       .ToListAsync();
        }

        public async Task<CustomerDTO> PostAsync(CustomerDTO obj)
        {
            _ = await _db.Customers.FirstOrDefaultAsync(x => x.Email == obj.Email
                                                        && x.FirstName == obj.FirstName
                                                        && x.LastName == obj.LastName
                                                        && x.IsDeleted == false) != null ?
                                                        throw new AppException(Constants.CUSTOMER_EXISTS) : 0;

            _ = await _db.Customers.FirstOrDefaultAsync(x => x.Email == obj.Email
                                                        && x.IsDeleted == false) != null ?
                                                        throw new AppException(Constants.EMAIL_EXISTS) : 0;

            CustomerDTO result = null;
            var newCustomer = obj.GetEntity();

            var deletedCustomer = await _db.Customers.Include(x => x.Parcels)
                                                     .ThenInclude(x => x.Category)
                                                     .Include(x => x.Parcels).ThenInclude(x => x.Shipment).ThenInclude(x => x.Status)
                                                     .Include(x => x.Address).IgnoreQueryFilters()
                                                     .FirstOrDefaultAsync(x => x.Email == obj.Email && x.IsDeleted == true);
            if (deletedCustomer == null)
            {
                if (await IsInvalidCustomer(obj.AddressId, obj.FirstName, obj.LastName, obj.Email, obj.Password))
                {
                    throw new AppException(Constants.INCORRECT_DATA);
                }

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
            _ = await _db.Customers.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Email == obj.Email)
                != null ? throw new AppException(Constants.CUSTOMER_EXISTS) : 0;

            var model = await _db.Customers.Include(c => c.Address).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.CUSTOMER_NOT_FOUND);

            if (await IsInvalidCustomer(obj.AddressId, obj.FirstName, obj.LastName, obj.Email, obj.Password))
            {
                throw new AppException(Constants.INCORRECT_DATA);
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
            return await _db.Customers.Where(x => x.Email.Contains(part))
                                      .Include(c => c.Address)
                                      .ThenInclude(c=> c.City)
                                      .ThenInclude(c=>c.Country)
                                      .Select(x => x.GetDTO()).ToListAsync();
        }

        public async Task<CustomerDTO> GetCustomerByIDAsync(int id)
        {
            return CustomerDTOMapperExtension.GetDTO(await _db.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id))
                ?? throw new AppException(Constants.CUSTOMER_NOT_FOUND);
        }

        private async Task<bool> IsInvalidCustomer(int addressId, string fName, string lName, string email, string pass)
        {
            var address = await _db.Addresses.AnyAsync(x => x.Id == addressId);
            var validEmail = Regex.IsMatch(email, @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPass = pass.Length >= 8 ? true : false;
            return !(address && !string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(lName) && validEmail && validPass);
        }
    }
}
