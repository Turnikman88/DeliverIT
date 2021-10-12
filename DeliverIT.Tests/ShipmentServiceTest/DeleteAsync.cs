using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ShipmentServiceTest
{
    [TestClass]
    public class DeleteAsync
    {
        [TestMethod]
        public async Task Success_When_DeleteAllShipmentsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_DeleteAllShipmentsAsync));

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
                var result = await sut.DeleteAsync(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, actContext.Shipments.Count());
            }
        }

        [TestMethod]
        public async Task Success_When_DeleteWithWrongIdAllShipmentsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_DeleteWithWrongIdAllShipmentsAsync));

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

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.DeleteAsync(100));
            }
        }
    }
}
