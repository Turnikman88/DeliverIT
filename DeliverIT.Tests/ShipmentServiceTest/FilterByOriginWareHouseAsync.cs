using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ShipmentServiceTest
{
    [TestClass]
    public class FilterByOriginWareHouseAsync
    {
        [TestMethod]
        public async Task Success_When_FilterByOriginWareHouseAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_FilterByOriginWareHouseAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();
            var parcels = Utils.GetParcels();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = await sut.FilterByOriginWareHouseAsync(1);

                Assert.AreEqual(2, result.Count());

                result = await sut.FilterByOriginWareHouseAsync(100);

                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public async Task Success_When_FilterByDestinationWareHouseAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_FilterByDestinationWareHouseAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();
            var parcels = Utils.GetParcels();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = await sut.FilterByDestinationWareHouseAsync(2);

                Assert.AreEqual(2, result.Count());

                result = await sut.FilterByDestinationWareHouseAsync(100);

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
