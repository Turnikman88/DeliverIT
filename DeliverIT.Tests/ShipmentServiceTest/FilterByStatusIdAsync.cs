using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ShipmentServiceTest
{
    [TestClass]
    public class FilterByStatusIdAsync
    {
        [TestMethod]
        public async Task Success_When_FilterByStatusIdAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_FilterByStatusIdAsync));

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
                var result = await sut.FilterByStatusIdAsync(1);

                Assert.AreEqual(2, result.Count());

                result = await sut.FilterByStatusIdAsync(2);

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
