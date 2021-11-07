using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DeliverIT.Services.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly DeliverITDBContext _db;

        public WareHouseService(DeliverITDBContext db)
        {
            this._db = db;
        }



        public async Task<WareHouseDTO> DeleteAsync(int id)
        {
            CheckId(id);

            var model = await this._db.WareHouses
                                    .Include(x => x.Parcels)
                                    .Include(w => w.Address)
                                        .ThenInclude(a => a.City)
                                            .ThenInclude(c => c.Country)
                                    .FirstOrDefaultAsync(x => x.Id == id)
                                    ?? throw new AppException(Constants.WAREHOUSE_NOT_FOUND);

            var modelGTO = model.GetDTO();

            model.DeletedOn = System.DateTime.Now;
            this._db.WareHouses.Remove(model);
            await _db.SaveChangesAsync();

            return modelGTO;
        }

        public async Task<IEnumerable<WareHouseDTO>> GetAsync()
        {
            var WareHousesDTO = await _db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                .Select(x => new WareHouseDTO
                {
                    Id = x.Id,
                    AddressId = x.AddressId,
                    StreetName = x.Address.StreetName,
                    City = x.Address.City.Name,
                    Country = x.Address.City.Country.Name,
                    Parcels = x.Parcels.Select(y => $"Id: {y.Id}; CustomerId {y.CustomerId}; ShipmentId {y.ShipmentId}").ToList()
                })
                .ToListAsync();

            return WareHousesDTO;
        }

        public async Task<IEnumerable<string>> GetAddressesAsync()
        {
            return await _db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                //.Select(x => $"Id: {x.Id}, Country: {x.Address.City.Country.Name}, City: {x.Address.City.Name}, Address: {x.Address.StreetName}")
                .Select(x => $"{x.Id}, {x.Address.City.Country.Name}, {x.Address.City.Name}, {x.Address.StreetName}")
                .ToListAsync();
        }

        public IQueryable<Address> GetAddressesObject()
        {
            var address = _db.WareHouses
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .Select(x => x.Address);
            return address;
        }

        public async Task<WareHouseDTO> GetWareHouseByIdAsync(int id)
        {
            CheckId(id);

            var model = await this._db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.Id == id)
                        ?? throw new AppException(Constants.WAREHOUSE_NOT_FOUND);

            var result = model.GetDTO();
            return result;
        }

        public async Task<IEnumerable<WareHouseDTO>> GetWareHouseByCountryAsync(string country)
        {
            var collection = await this._db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .Where(x => x.Address.City.Country.Name.Contains(country)).Select(x=>x.GetDTO()).ToListAsync()
                        ?? throw new AppException(Constants.WAREHOUSE_NOT_FOUND);

            return collection;
        }

        public async Task<IEnumerable<WareHouseDTO>> GetWareHouseByCityAsync(string city)
        {
            var collection = await this._db.WareHouses
               .Include(x => x.Parcels)
               .Include(w => w.Address)
                   .ThenInclude(a => a.City)
                       .ThenInclude(c => c.Country)
                       .Where(x => x.Address.City.Name.Contains(city)).Select(x => x.GetDTO()).ToListAsync()
                       ?? throw new AppException(Constants.WAREHOUSE_NOT_FOUND);

            return collection;
        }


        public async Task<WareHouseDTO> PostAsync(WareHouseDTO obj)
        {
            _ = await _db.WareHouses.FirstOrDefaultAsync(x => x.AddressId == obj.AddressId)
                != null ? throw new AppException(Constants.WAREHOUSE_ADDRESS_EXISTS) : 0;

            WareHouseDTO result = null;

            var wareHouse = obj.GetEntity();
            var deleteWareHouse = await _db.WareHouses.IgnoreQueryFilters()
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.AddressId == obj.AddressId && x.IsDeleted == true);

            if (deleteWareHouse == null)
            {
                await _db.WareHouses.AddAsync(wareHouse);
                await _db.SaveChangesAsync();
                wareHouse = await _db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.Id == wareHouse.Id);
                result = wareHouse.GetDTO();
            }
            else
            {
                deleteWareHouse.DeletedOn = null;
                deleteWareHouse.IsDeleted = false;
                await _db.SaveChangesAsync();
                result = deleteWareHouse.GetDTO();
            }

            return result;
        }

        public async Task<WareHouseDTO> UpdateAsync(int id, WareHouseDTO obj)
        {
            CheckId(id);
            var model = await this._db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.Id == id)
                        ?? throw new AppException(Constants.WAREHOUSE_NOT_FOUND);

            CheckId(obj.AddressId);

            model.AddressId = obj.AddressId;
            var result = model.GetDTO();

            await _db.SaveChangesAsync();

            return result;
        }

        private static void CheckId(int id)
        {
            if (id <= 0)
            {
                throw new AppException(Constants.INVALID_ID);
            }
        }

        public async Task<int> GetCountAsync()
        {
            return await _db.WareHouses.CountAsync();
        }
    }
}
