using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class SortByArrivingDayAsync
    {
        [TestMethod]
        public async Task Success_When_SortByArrivingDayAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_SortByArrivingDayAsync));

            var parcels = Utils.GetParcels();
            parcels.Add(new Parcel
            {
                Id = 2,
                CustomerId = 1,
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

                Assert.AreEqual(1, actContext.Parcels.First().Id);

                var result = await sut.SortByArrivalDateAsync();

                Assert.AreEqual(2, result.First().Id);
            }
        }
    }
}
