using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class ListCustomerIncomingParcelsAsync
    {
        [TestMethod]
        public async Task Success_When_ListCustomerIncomingParcelsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_ListCustomerIncomingParcelsAsync));

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
                var result = await sut.ListCustomerIncomingParcelsAsync(1);

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
