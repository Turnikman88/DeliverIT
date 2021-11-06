using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class SortByWeightAndArrivalDateAsync
    {
        [TestMethod]
        public async Task Success_When_SortByWeight_And_ArrivingDayAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_SortByWeight_And_ArrivingDayAsync));

            var parcels = Utils.GetParcels();
            parcels.Add(new Parcel
            {
                Id = 2,
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1,
                DeliverToAddress = true
            });
            parcels.Add(new Parcel
            {
                Id = 3,
                CustomerId = 1,
                ShipmentId = 2,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1,
                DeliverToAddress = true
            });
            var category = Utils.GetCategories();
            var shipments = Utils.GetShipments();
            var status = Utils.GetStatuses();
            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.AreEqual(1, actContext.Parcels.First().Id);

                var result = await sut.SortByWeightAndArrivalDateAsync();

                Assert.AreEqual(3, result.First().Id);
            }
        }
    }
}
