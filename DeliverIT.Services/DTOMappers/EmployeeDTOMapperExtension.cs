using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Services.DTOMappers
{
    public static class EmployeeDTOMapperExtension
    {
        public static EmployeeDTO GetDTO(this Employee employee)
        {
            if (employee is null || employee.FirstName is null
                || employee.LastName is null || employee.Password is null
                || employee.Email is null || employee.AddressId <= 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                AddressId = employee.AddressId,
                Address = employee.Address?.StreetName,
                City = employee.Address?.City.Name,
                Country = employee.Address?.City.Country.Name,
                Email = employee.Email,
                Password = employee.Password
            };
        }

        public static Employee GetEntity(this EmployeeDTO employee)
        {
            if (employee is null || employee.FirstName is null
                || employee.LastName is null || employee.Password is null
                || employee.Email is null || employee.AddressId <= 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }
            return new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                AddressId = employee.AddressId,
                Email = employee.Email,
                Password = employee.Password
            };
        }
    }
}
