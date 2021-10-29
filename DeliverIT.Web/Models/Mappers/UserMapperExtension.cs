using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Web.Models.Mappers
{
    public static class UserMapperExtension
    {
        public static UserViewModel GetModel(this CustomerDTO customer) 
        {
            return new UserViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password,
                Address = customer.Address,
                City = customer.City, 
                Country = customer.Country
            };
        }

        public static UserViewModel GetModel(this EmployeeDTO employee)
        {
            return new UserViewModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Password = employee.Password,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country
            };
        }

        public static CustomerDTO GetDTO(this UserViewModel user)
        {
            return new CustomerDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                AddressId = user.AddressId
            };
        }
    }
}
