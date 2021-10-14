using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ShipmentServiceTest
{
    [TestClass]
    public class UpdateAsync
    {
        [TestMethod]
        public async Task Success_When_UpdateShipmentsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_UpdateShipmentsAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();

            var parcels = Utils.GetParcels();

            var warehouses = Utils.GetWareHouses();

            var dto = new ShipmentDTO
            {
                DepartureDate = DateTime.Today.AddDays(5).ToString(),
                ArrivalDate = DateTime.Today.AddDays(10).ToString(),
                OriginWareHouseId = 1,
                DestinationWareHouseId = 2,
                StatusId = 3
            };

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.WareHouses.AddRangeAsync(warehouses);
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.AreEqual(1, actContext.Shipments.Skip(1).First().StatusId);

                var result = await sut.UpdateAsync(2, dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, actContext.Shipments.Count());
                Assert.AreEqual(3, actContext.Shipments.Skip(1).First().StatusId);
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateShipmentsNullAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateShipmentsNullAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();

            var parcels = Utils.GetParcels();

            var warehouses = Utils.GetWareHouses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.WareHouses.AddRangeAsync(warehouses);
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(1, null));
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateShipmentWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateShipmentWrongIdAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();

            var parcels = Utils.GetParcels();

            var warehouses = Utils.GetWareHouses();

            var dto = new ShipmentDTO
            {
                DepartureDate = DateTime.Today.AddDays(5).ToString(),
                ArrivalDate = DateTime.Today.AddDays(10).ToString(),
                OriginWareHouseId = 1,
                DestinationWareHouseId = 2,
                StatusId = 3
            };

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.WareHouses.AddRangeAsync(warehouses);
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(100, dto));
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateShipmentWrongDateAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateShipmentWrongDateAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();

            var parcels = Utils.GetParcels();

            var warehouses = Utils.GetWareHouses();

            var dto = new ShipmentDTO
            {
                DepartureDate = "2018 - 12 - 0243",
                ArrivalDate = DateTime.Today.AddDays(10).ToString(),
                OriginWareHouseId = 1,
                DestinationWareHouseId = 2,
                StatusId = 3
            };

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.WareHouses.AddRangeAsync(warehouses);
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(1, dto));
            }
        }
    }
}
