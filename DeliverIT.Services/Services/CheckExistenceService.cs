using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Services
{
    public class CheckExistenceService : ICheckExistenceService
    {
        private readonly DeliverITDBContext _db;

        public CheckExistenceService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public async Task<bool> CountryExists(string name)
        {
            return await _db.Countries.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> CityExists(string name, int countryId)
        {
            return await _db.Cities.AnyAsync(x => x.Name == name && x.CountryId == countryId);
        }

        public async Task<bool> WarehouseExists(int? id)
        {
            return await _db.WareHouses.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ShipmentExists(int id)
        {
            return await _db.Shipments.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> CustomerExists(int id)
        {
            return await _db.Customers.AnyAsync(x => x.Id == id);
        }
    }
}
