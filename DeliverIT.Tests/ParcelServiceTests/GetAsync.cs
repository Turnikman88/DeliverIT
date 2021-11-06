using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class GetAsync
    {
        [TestMethod]
        public async Task Success_When_GetAllParcelsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_GetAllParcelsAsync));

            var parcels = Utils.GetParcels();
            var category = Utils.GetCategories();
            var shipments = Utils.GetShipments();
            var statuses = Utils.GetStatuses();
            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Shipments.AddRangeAsync(shipments);

                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(1, result.Count());
            }
        }

        [TestMethod]
        public async Task Empty_When_GetAllParcelsAsyncTest()
        {
            var options = Utils.GetOptions(nameof(Empty_When_GetAllParcelsAsyncTest));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
