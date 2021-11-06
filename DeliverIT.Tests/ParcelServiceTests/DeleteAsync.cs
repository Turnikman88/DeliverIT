using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class DeleteAsync
    {
        [TestMethod]
        public async Task Success_When_DeleteAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_DeleteAsync));

            var parcels = Utils.GetParcels();
            var category = Utils.GetCategories();
            var statuses = Utils.GetStatuses();
            var shipments = Utils.GetShipments();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = await sut.DeleteAsync(1);

                Assert.AreEqual(parcels.Count - 1, actContext.Parcels.Count());
            }
        }

        [TestMethod]
        public async Task Throws_When_DeleteWithWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_DeleteWithWrongIdAsync));

            var parcels = Utils.GetParcels();
            var category = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.DeleteAsync(100));
            }
        }
    }
}
