using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.WarehouseServiceTest
{
    [TestClass]
    public class UpdateWarehouse
    {
        [TestMethod]
        public async Task ShouldUpdateObject()
        {
            var options = Utils.GetOptions(nameof(ShouldUpdateObject));

            var address = Utils.GetAddresses();
            var parcels = Utils.GetParcels();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var warehouse = Utils.GetWareHouses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.WareHouses.AddRangeAsync(warehouse);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new WareHouseService(actContext);

                var expected = actContext.WareHouses.FindAsync(1);

                var testObject = new WareHouseDTO()
                {

                    AddressId = 2
                };

                var result = await sut.UpdateAsync(1, testObject);

                Assert.AreEqual(testObject.AddressId, result.AddressId);
                Assert.AreEqual(expected.Result.AddressId, result.AddressId);

            }
        }
    }
}
