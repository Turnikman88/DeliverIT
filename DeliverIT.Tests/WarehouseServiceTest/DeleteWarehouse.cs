using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.WarehouseServiceTest
{
    [TestClass]
    public class DeleteWarehouse
    {
        [TestMethod]
        public async Task ShouldDeleteCorrectItems()
        {
            var options = Utils.GetOptions(nameof(DeleteWarehouse));

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
                var testObject = actContext.WareHouses.First();

                var result = await sut.DeleteAsync(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.Id);
                Assert.AreEqual(testObject.AddressId, result.AddressId);
            }
        }
    }
}
