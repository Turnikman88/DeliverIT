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
    public class PostAsync
    {
        [TestMethod]
        public async Task Success_When_PostShipmentsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_PostShipmentsAsync));

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

                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(3, actContext.Shipments.Count());
                Assert.AreEqual(3, actContext.Shipments.Skip(2).First().StatusId);
            }
        }

        [TestMethod]
        public async Task Success_When_PostExistingShipmentsAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_PostExistingShipmentsAsync));

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
                StatusId = 1
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
                var deleted = actContext.Shipments.First();
                deleted.IsDeleted = true;
                await actContext.SaveChangesAsync();

                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, actContext.Shipments.Count());
                Assert.AreEqual(1, actContext.Shipments.First().StatusId);
            }
        }

        [TestMethod]
        public async Task Throws_When_PostShipmentsWrongDataAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostShipmentsWrongDataAsync));

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
                StatusId = 333
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

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.PostAsync(dto));
            }
        }
    }
}
