using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class GetParcelByIdAsync
    {
        [TestMethod]
        public async Task Success_When_GetParcelByIdAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_GetParcelByIdAsync));

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
                var result = await sut.GetParcelByIdAsync(1);
                                
                Assert.AreEqual(1, result.Id);
            }
        }

        [TestMethod]
        public async Task Throws_When_GetParcelByWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_GetParcelByWrongIdAsync));

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

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.GetParcelByIdAsync(100));
            }
        }
    }
}
