using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.WarehouseServiceTest
{
    [TestClass]
    public class PostAyncWarehouse
    {
        [TestMethod]
        public async Task ShouldCreateObject()
        {
            var options = Utils.GetOptions(nameof(ShouldCreateObject));

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
                var testObject = new WareHouseDTO()
                {
                    Id = 100,
                    City = "Istanbul",
                    Country = "Turkey",
                    StreetName = "TestName",
                    AddressId = 3
                };

                var result = await sut.PostAsync(testObject);

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.Id);
                Assert.AreEqual(testObject.City, result.City);
                Assert.AreEqual(testObject.AddressId, result.AddressId);

                var alreadyDeleted = actContext.WareHouses.Find(100);
                alreadyDeleted.IsDeleted = true;
                await actContext.SaveChangesAsync();

                var resultAlreadyDeleted = await sut.PostAsync(testObject);

                Assert.AreEqual(testObject.Id, result.Id);
                Assert.AreEqual(testObject.City, result.City);
                Assert.AreEqual(testObject.AddressId, result.AddressId);
            }
        }
    }
}
