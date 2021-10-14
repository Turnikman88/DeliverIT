using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class PostAsync
    {
        [TestMethod]
        public async Task Success_When_PostParcelAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_PostParcelAsync));

            var dto = new ParcelDTO
            {
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 55,
                DeliverToAddress = true
            };

            var parcels = Utils.GetParcels();
            var customers = Utils.GetCustomers();
            var shipments = Utils.GetShipments();
            var warehouses = Utils.GetWareHouses();
            var category = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.WareHouses.AddRangeAsync(warehouses);
                await arrangeContext.Categories.AddRangeAsync(category);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, actContext.Parcels.Count());
            }
        }
        [TestMethod]
        public async Task DeletedParcel_GetsItsId()
        {
            var options = Utils.GetOptions(nameof(DeletedParcel_GetsItsId));

            var dto = new ParcelDTO
            {
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1234.56,
                DeliverToAddress = true
            };

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
                var deleted = actContext.Parcels.FirstOrDefault(x => x.Id == 1);
                deleted.IsDeleted = true;
                await actContext.SaveChangesAsync();

                var sut = new ParcelService(actContext);

                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, actContext.Parcels.Count());
                Assert.AreEqual(1, result.Id);
            }
        }

        [TestMethod]
        public async Task Throws_When_PostParcelWithWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostParcelWithWrongIdAsync));

            var dto = new ParcelDTO
            {
                CustomerId = 100,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 55,
                DeliverToAddress = true
            };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.PostAsync(dto));
            }
        }
    }
}
