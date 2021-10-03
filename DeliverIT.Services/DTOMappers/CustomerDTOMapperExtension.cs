using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Services.DTOMappers
{
    public static class CustomerDTOMapperExtension
    {

        public static CustomerDTO GetDTO(this Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AddressId = customer.AddressId,
                Email = customer.Email
            };
        }

        public static Customer GetEntity(this CustomerDTO customer)
        {
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AddressId = customer.AddressId,
                Email = customer.Email
            };
        }
    }
}
