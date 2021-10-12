using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
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
    public class UpdateAsync
    {
        [TestMethod]
        public async Task Success_When_UpdateParcelAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_UpdateParcelAsync));

            var dto = new ParcelDTO
            {
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 555,
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
                var result = await sut.UpdateAsync(1, dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(555, result.Weight);
                Assert.AreEqual(parcels.Count(), actContext.Parcels.Count());
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateParcelWithWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateParcelWithWrongIdAsync));

            var dto = new ParcelDTO
            {
                CustomerId = 1,
                ShipmentId = 1,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 555,
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

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(100, dto));
            }
        }
    }
}
