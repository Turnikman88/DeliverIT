using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.WarehouseServiceTest
{
    [TestClass]
    public class GetWarehouse
    {
        [TestMethod]
        public async Task GetAllWarehouse()
        {
            var options = Utils.GetOptions(nameof(GetAllWarehouse));

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
                var result = await sut.GetAsync();

                Assert.AreEqual(warehouse.Count(), result.Count());
            }
        }

        [TestMethod]
        public async Task GetAllWarehouseAddresses()
        {
            var options = Utils.GetOptions(nameof(GetAllWarehouseAddresses));
            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var address = Utils.GetAddresses();
            var warehouse = Utils.GetWareHouses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.WareHouses.AddRangeAsync(warehouse);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new WareHouseService(actContext);
                var result = await sut.GetAddressesAsync();

                Assert.AreEqual(warehouse.Count(), result.Count());
            }
        }

        [TestMethod]
        public async Task GetAllWarehouseById()
        {
            var options = Utils.GetOptions(nameof(GetAllWarehouseById));

            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var address = Utils.GetAddresses();
            var warehouse = Utils.GetWareHouses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.WareHouses.AddRangeAsync(warehouse);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new WareHouseService(actContext);
                var result = await sut.GetWareHouseByIdAsync(1);

                var testObject = actContext.WareHouses.First();

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.Id);
                Assert.AreEqual(testObject.AddressId, result.AddressId);
            }
        }
    }
}
