using DeliverIT.Models;
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
    public class DeleteAsync
    {
        [TestMethod]
        public async Task Success_When_DeleteAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_DeleteAsync));

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
                var result = await sut.DeleteAsync(1);

                Assert.AreEqual(parcels.Count - 1, actContext.Parcels.Count());
            }
        }
    }
}
