using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIT.Services.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly DeliverITDBContext db;

        public WareHouseService(DeliverITDBContext db)
        {
            this.db = db;
        }
        public IEnumerable<WareHouseDTO> Locations()
        {
            var WareHouseDTO = db.WareHouses.Include(x => x.Address).Select(x => new WareHouseDTO {Id = x.Id, StreetName = x.Address.StreetName }).ToList();
            return WareHouseDTO;            
        }
    }
}
