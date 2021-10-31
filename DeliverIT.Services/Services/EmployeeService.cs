using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DeliverITDBContext _db;
        public EmployeeService(DeliverITDBContext db)
        {
            this._db = db;
        }
        public async Task<EmployeeDTO> DeleteAsync(int id)
        {
            var employee = await _db.Employees.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.EMPLOYEE_NOT_FOUND);

            var employeDTO = employee.GetDTO();

            employee.DeletedOn = DateTime.Now;
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            return employeDTO;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAsync()
        {
            return await _db.Employees.Include(x => x.Address)
                                      .Select(x => x.GetDTO())
                                      .ToListAsync();
        }

        public async Task<EmployeeDTO> PostAsync(EmployeeDTO obj)
        {
            _ = await _db.Employees.FirstOrDefaultAsync(x => x.Email == obj.Email
                                                        && x.FirstName == obj.FirstName
                                                        && x.LastName == obj.LastName
                                                        && x.IsDeleted == false) != null ?
                                                        throw new AppException(Constants.EMPLOYEE_EXISTS) : 0;

            _ = await _db.Employees.FirstOrDefaultAsync(x => x.Email == obj.Email
                                                        && x.IsDeleted == false) != null ?
                                                        throw new AppException(Constants.EMAIL_EXISTS) : 0;

            EmployeeDTO result = null;
            var newEmployee = obj.GetEntity();

            var deletedEmployee = await _db.Employees.Include(x => x.Address).IgnoreQueryFilters()
                                                     .FirstOrDefaultAsync(x => x.Email == obj.Email && x.IsDeleted == true);
            if (deletedEmployee == null)
            {
                if (await IsInvalidEmployee(obj.AddressId, obj.FirstName, obj.LastName, obj.Email, obj.Password))
                {
                    throw new AppException(Constants.INCORRECT_DATA);
                }

                await _db.Employees.AddAsync(newEmployee);
                await this._db.SaveChangesAsync();

                newEmployee = await this._db.Employees.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == newEmployee.Id);
                result = newEmployee.GetDTO();
            }
            else
            {
                deletedEmployee.DeletedOn = null;
                deletedEmployee.IsDeleted = false;
                await this._db.SaveChangesAsync();
                result = deletedEmployee.GetDTO();
            }

            return result;
        }

        public async Task<EmployeeDTO> GetEmployeeByEmail(string email)
        {
            var employee = await _db.Employees.Where(x => x.Email.Contains(email))
                                      .Include(x => x.Address).FirstOrDefaultAsync();
            return  employee.GetDTO();
        }

        public async Task<EmployeeDTO> UpdateAsync(int id, EmployeeDTO obj)
        {
            _ = await _db.Employees.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Email == obj.Email)
                != null ? throw new AppException(Constants.CUSTOMER_EXISTS) : 0;

            var model = await _db.Employees.Include(c => c.Address).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AppException(Constants.EMPLOYEE_NOT_FOUND);

            if (await IsInvalidEmployee(obj.AddressId, obj.FirstName, obj.LastName, obj.Email, obj.Password))
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            model.FirstName = obj.FirstName;
            model.LastName = obj.LastName;
            model.AddressId = obj.AddressId ?? null;
            model.Email = obj.Email;
            model.Password = obj.Password;

            await _db.SaveChangesAsync();

            return model.GetDTO();
        }

        private async Task<bool> IsInvalidEmployee(int? addressId, string fName, string lName, string email, string pass)
        {
            var address = await _db.Addresses.AnyAsync(x => x.Id == addressId);
            var validEmail = Regex.IsMatch(email, @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPass = pass.Length >= 8 ? true : false;
            return !(address && !string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(lName) && validEmail && validPass);
        }
    }
}
