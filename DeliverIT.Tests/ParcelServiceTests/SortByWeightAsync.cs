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
    public class SortByWeightAsync
    {
        [TestMethod]
        public async Task Success_When_SortByWeightAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_SortByWeightAsync));

            var parcels = Utils.GetParcels();
            parcels.Add(new Parcel {
                Id = 2,
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1,
                DeliverToAddress = true
            });
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

                Assert.AreEqual(1234.56, actContext.Parcels.First().Weight);

                var result = await sut.SortByWeightAsync();

                Assert.AreEqual(1, result.First().Weight);
            }
        }
    }
}
