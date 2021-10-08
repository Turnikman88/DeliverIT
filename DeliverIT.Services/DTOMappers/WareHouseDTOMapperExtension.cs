using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using System.Linq;

namespace DeliverIT.Services.DTOMappers
{
    public static class WareHouseDTOMapperExtension
    {
        public static WareHouseDTO GetDTO(this WareHouse wareHouse)
        {
            if (wareHouse is null || wareHouse.AddressId <= 0 || wareHouse.Parcels == null
                || wareHouse.Address == null)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            return new WareHouseDTO
            {
                Id = wareHouse.Id,
                AddressId = wareHouse.AddressId,
                StreetName = wareHouse.Address.StreetName,
                City = wareHouse.Address.City.Name,
                Country = wareHouse.Address.City.Country.Name,
                Parcels = wareHouse.Parcels.Select(x => $"Id: {x.Id}; CustomerId {x.CustomerId}; ShipmentId {x.ShipmentId}").ToList()
                //ToDo: maybe add other collections
            };

        }

        public static WareHouse GetEntity(this WareHouseDTO wareHouse)
        {
            if (wareHouse is null || wareHouse.AddressId <= 0)
            {
                throw new AppException(Constants.INCORRECT_DATA);
            }

            return new WareHouse
            {
                Id = wareHouse.Id,
                AddressId = wareHouse.AddressId
            };
        }
    }
}
