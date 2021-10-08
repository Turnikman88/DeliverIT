using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using System.Linq;

namespace DeliverIT.Services.DTOMappers
{
    public static class CustomerDTOMapperExtension
    {

        public static CustomerDTO GetDTO(this Customer customer)
        {
            if (customer is null || customer.FirstName is null
                || customer.LastName is null || customer.Password is null
                || customer.Email is null || customer.AddressId <= 0)
            {
                throw new AppException(Constants.INVALID_OBJECT);
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AddressId = customer.AddressId,
                Address = customer.Address.StreetName,
                Parcels = customer.Parcels?
                    .Select(x => ($"Category: {x.Category.Name} Shipment Status: {x.Shipment.Status.Name} " +
                                  $"Departure date: {x.Shipment.DepartureDate.ToString("dd/MM/yyyy")} " +
                                  $"Weight: {x.Weight} Will be delivered to you: {x.DeliverToAddress} " +
                                  $"on date {x.Shipment.ArrivalDate.ToString("dd/MM/yyyy")}"))
                    .ToList(),
                Email = customer.Email,
                Password = customer.Password
            };
        }

        public static Customer GetEntity(this CustomerDTO customer)
        {
            if (customer is null || customer.FirstName is null
                || customer.LastName is null || customer.Password is null
                || customer.Email is null || customer.AddressId <= 0)
            {
                throw new AppException(Constants.INVALID_OBJECT);
            }
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AddressId = customer.AddressId,
                Email = customer.Email,
                Password = customer.Password
            };
        }
    }
}
