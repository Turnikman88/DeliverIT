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
            if (wareHouse is null || wareHouse.Address is null)
            {
                throw new AppException(Constants.INVALID_OBJECT);
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

        public static WareHouse GetEntity(this WareHouseDTO wareHouseDTO)
        {
            if (wareHouseDTO is null || wareHouseDTO.AddressId <= 0)
            {
                throw new AppException(Constants.INVALID_OBJECT);
            }
            return new WareHouse
            {
                Id = wareHouseDTO.Id,
                AddressId = wareHouseDTO.AddressId
            };
        }
    }
}
