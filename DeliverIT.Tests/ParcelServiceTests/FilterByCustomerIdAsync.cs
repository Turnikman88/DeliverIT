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
    public class FilterByCustomerIdAsync
    {
        [TestMethod]
        public async Task Success_When_FilterByCustomerIdAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_FilterByCustomerIdAsync));

            var parcels = Utils.GetParcels();
            parcels.Add(new Parcel
            {
                Id = 2,
                CustomerId = 2,
                ShipmentId = 2,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1,
                DeliverToAddress = true
            });
            var category = Utils.GetCategories();
            var shipments = Utils.GetShipments();
            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.AreEqual(2, actContext.Parcels.Count());

                var result = await sut.FilterByCustomerIdAsync(1);

                Assert.AreEqual(1, result.Count());
            }
        }
    }
}
