using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
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
        public IEnumerable<WareHouse> Locations()
        {
            return db.WareHouses.ToList();//TODO: JOIN Addresses
        }
    }
}
