using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class GetShipmentStatusAsync
    {
        [TestMethod]
        public async Task Success_When_GetShipmentStatusAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_GetShipmentStatusAsync));

            var parcels = Utils.GetParcels();
           
            var statuses = Utils.GetStatuses();
            var customers = Utils.GetCustomers();
            var shipments = Utils.GetShipments();
            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                var result = await sut.GetShipmentStatusAsync(1);

                Assert.AreEqual($"Id: 1, Preparing", result.First());
            }
        }
    }
}
