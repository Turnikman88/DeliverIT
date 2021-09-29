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

        public async Task<WareHouseDTO> Delete(int id)
        {
            var model = await this.db.WareHouses.FindAsync(id);
            var modelGTO = model.GetDTO();
            this.db.WareHouses.Remove(model);
            await db.SaveChangesAsync();

            return modelGTO;
        }

        public async Task<IEnumerable<WareHouseDTO>> Get()
        {
            var WareHousesDTO = await db.WareHouses
                .Select(x => new WareHouseDTO
                { 
                    Id = x.Id,
                    StreetName = x.Address.StreetName,
                    City = x.Address.City.Name,
                    Country = x.Address.City.Country.Name
                })
                .ToListAsync();

            return WareHousesDTO;
        }

        public async Task<WareHouseDTO> Post(WareHouseDTO obj)
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

            var model = new WareHouse
            {
                AddressId = address.Id
            };
            
            var newWareHouse = await db.WareHouses.AddAsync(model);
            await db.SaveChangesAsync();
            obj.Id = await db.WareHouses.Where(x => x.AddressId == address.Id).Select(x => x.Id).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<WareHouseDTO> Update(int id, WareHouseDTO obj) 
        {
            var model = await this.db.WareHouses.FindAsync(id);     
            var newAddress = await this.db.Addresses.AddAsync(new Address { StreetName = obj.StreetName })
            model.Address.StreetName = obj.StreetName;
            obj.Id = model.Id;
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> WareHouseExists(int id) 
        {
            var model = await db.WareHouses.FirstOrDefaultAsync(x => x.Id == id);
            return model is null ? false : true;
        }

    }
}
