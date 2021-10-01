using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverIT.Services.DTOMappers;
namespace DeliverIT.Services.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly DeliverITDBContext db;

        public WareHouseService(DeliverITDBContext db)
        {
            this.db = db;
        }

        public async Task<WareHouseDTO> DeleteAsync(int id)
        {
            var model = await this.db.WareHouses
                                    .Include(x => x.Parcels)
                                    .Include(w => w.Address)
                                        .ThenInclude(a => a.City)
                                            .ThenInclude(c => c.Country)
                                    .FirstOrDefaultAsync(x => x.Id == id);
            var modelGTO = model.GetDTO();

            model.DeletedOn = System.DateTime.Now;
            this.db.WareHouses.Remove(model);
            await db.SaveChangesAsync();

            return modelGTO;
        }

        public async Task<IEnumerable<WareHouseDTO>> GetAsync()
        {
            var WareHousesDTO = await db.WareHouses
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

        public async Task<WareHouseDTO> GetWareHouseByIdAsync(int id)
        {
            var model = await this.db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.Id == id);

            var result = model.GetDTO();
            return result;
        }

        public async Task<WareHouseDTO> PostAsync(WareHouseDTO obj)
        {
            WareHouseDTO result = null;

            var wareHouse = obj.GetEntity();
            var deleteWareHouse = await db.WareHouses.IgnoreQueryFilters()
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.AddressId == obj.AddressId && x.IsDeleted == true);

            if (deleteWareHouse == null)
            {
                await db.WareHouses.AddAsync(wareHouse);
                await db.SaveChangesAsync();
                wareHouse = await db.WareHouses
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
                await db.SaveChangesAsync();
                result = deleteWareHouse.GetDTO();
            }

            return result;
        }

        public async Task<WareHouseDTO> UpdateAsync(int id, WareHouseDTO obj)
        {
            var model = await this.db.WareHouses
                .Include(x => x.Parcels)
                .Include(w => w.Address)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(x => x.Id == id);

            model.AddressId = obj.AddressId;
            var result = model.GetDTO();

            await db.SaveChangesAsync();

            return result;
        }

        public async Task<bool> WareHouseExistsAsync(int id)
        {
            var model = await db.WareHouses.FirstOrDefaultAsync(x => x.Id == id);
            return model is null ? false : true;
        }
        /*private async Task<int> GetAddresId(WareHouseDTO obj)
        {
            var address = await db.Addresses.FirstOrDefaultAsync(x => x.StreetName == obj.StreetName);
            var city = await db.Cities.FirstOrDefaultAsync(x => x.Name == obj.City);
            var country = await db.Countries.FirstOrDefaultAsync(x => x.Name == obj.Country);

            if (country is null)
            {
                await this.db.Countries.AddAsync(new Country { Name = obj.Country });
                await db.SaveChangesAsync();

            }
            if (city is null)
            {
                await this.db.Cities.AddAsync(new City
                {
                    CountryId = await db.Countries.Where(x => x.Name == obj.Country).Select(x => x.Id).FirstOrDefaultAsync(),
                    Name = obj.City
                });
                await db.SaveChangesAsync();

            }

            if (address is null)
            {
                address = new Address
                {
                    CityId = await db.Cities.Where(x => x.Name == obj.City).Select(x => x.Id).FirstOrDefaultAsync(),
                    StreetName = obj.StreetName
                };
                await this.db.Addresses.AddAsync(address);
                await db.SaveChangesAsync();
            }

            return address.Id;
        }*/
    }
}
