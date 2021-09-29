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
    public static class WareHouseMapper
    {
        //private static DeliverITDBContext db = new DeliverITDBContext();

        public static WareHouseDTO GetDTO(this WareHouse wareHouse) //ToDo: why address id is null
        {
            var a = new WareHouseDTO
            {
                Id = wareHouse.Id,
                StreetName = wareHouse.Address.StreetName,
                City = wareHouse.Address.City.Name,
                Country = wareHouse.Address.City.Country.Name
            };
            return a;
        }
    }
}
