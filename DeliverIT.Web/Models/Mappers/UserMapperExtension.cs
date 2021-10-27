using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Web.Models.Mappers
{
    public static class UserMapperExtension
    {
        public static UserViewModel GetModel(this Customer customer)
        {
            return new UserViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password,
                Address = customer.Address.StreetName,
                City = customer.Address.City.Name,
                Country = customer.Address.City.Country.Name

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
