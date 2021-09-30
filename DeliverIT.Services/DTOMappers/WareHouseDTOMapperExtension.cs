using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.DTOMappers
{
    public static class WareHouseDTOMapperExtension
    {
        public static WareHouseDTO GetDTO(this WareHouse wareHouse) 
        {
            return new WareHouseDTO
            {
                Id = wareHouse.Id,
                AddressId = wareHouse.AddressId,
                StreetName = wareHouse.Address.StreetName,
                City = wareHouse.Address.City.Name,
                Country = wareHouse.Address.City.Country.Name,
                Parcels = wareHouse.Parcels.Select(x =>  $"Id: {x.Id}; CustomerId {x.CustomerId}; ShipmentId {x.ShipmentId}" ).ToList()
                //ToDo: maybe add other collections
            };
           
        }

        public static WareHouse GetEntity(this WareHouseDTO wareHouseDTO)
        {
            return new WareHouse
            {
                Id = wareHouseDTO.Id,
                AddressId = wareHouseDTO.AddressId
            };
        }
    }
}
