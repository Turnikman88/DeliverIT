using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class MultiFilter
    {
        [TestMethod]
        public async Task Success_When_LookingForMultipleOptions()
        {
            var options = Utils.GetOptions(nameof(Success_When_LookingForMultipleOptions));

            var parcels = Utils.GetParcels();
            var customers = Utils.GetCustomers();
            var shipment = Utils.GetShipments();
            var category = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.Shipments.AddRangeAsync(shipment);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                var testObject = actContext.Parcels.FirstOrDefault(x => x.Id == 1);
                var result = await sut.MultiFilterAsync(1, 1, null, null, null, "Electronics", 1200, null);

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.First().Id);
                Assert.AreEqual(testObject.WareHouseId, result.First().WareHouseId);
                Assert.AreEqual(testObject.ShipmentId, result.First().ShipmentId);
                Assert.AreEqual(testObject.Weight, result.First().Weight);
                Assert.AreEqual(testObject.Category.Name, result.First().CategoryName);
            }
        }

        [TestMethod]
        public async Task Success_When_LookingForMultipleOptions_NoID()
        {
            var options = Utils.GetOptions(nameof(Success_When_LookingForMultipleOptions));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                var testObject = actContext.Parcels.FirstOrDefault(x => x.CustomerId == 1);
                var result = await sut.MultiFilterAsync(null, 1, null, null, null, "Electronics", 1200, null);

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.First().Id);
                Assert.AreEqual(testObject.WareHouseId, result.First().WareHouseId);
                Assert.AreEqual(testObject.ShipmentId, result.First().ShipmentId);
                Assert.AreEqual(testObject.Weight, result.First().Weight);
                Assert.AreEqual(testObject.Category.Name, result.First().CategoryName);
            }
        }

        [TestMethod]
        public async Task Success_When_LookingForMultipleOptions_ShipmentId()
        {
            var options = Utils.GetOptions(nameof(Success_When_LookingForMultipleOptions));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                var testObject = actContext.Parcels.FirstOrDefault(x => x.ShipmentId == 1);
                var result = await sut.MultiFilterAsync(null, null, 1, null, null, "Electronics", null, 1500);

                Assert.IsNotNull(result);
                Assert.AreEqual(testObject.Id, result.First().Id);
                Assert.AreEqual(testObject.WareHouseId, result.First().WareHouseId);
                Assert.AreEqual(testObject.ShipmentId, result.First().ShipmentId);
                Assert.AreEqual(testObject.Weight, result.First().Weight);
                Assert.AreEqual(testObject.Category.Name, result.First().CategoryName);
            }
        }

    }
}
