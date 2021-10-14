using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ParcelServiceTests
{
    [TestClass]
    public class ChangeDeliverLocationAsync
    {
        [TestMethod]
        public async Task Success_When_ChangeDeliverLocationAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_ChangeDeliverLocationAsync));

            var parcels = Utils.GetParcels();

            var statuses = Utils.GetStatuses();
            var customers = Utils.GetCustomers();
            var shipments = Utils.GetShipments();
            var categories = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(categories);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.AreEqual(true, actContext.Parcels.First().DeliverToAddress);

                var result = await sut.ChangeDeliverLocationAsync(1);

                Assert.AreEqual(false, actContext.Parcels.First().DeliverToAddress);
            }
        }

        [TestMethod]
        public async Task Throws_When_ChangeDeliverLocationWithWrongIdAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ChangeDeliverLocationWithWrongIdAsync));

            var parcels = Utils.GetParcels();

            var statuses = Utils.GetStatuses();
            var customers = Utils.GetCustomers();
            var shipments = Utils.GetShipments();
            var categories = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(categories);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.ChangeDeliverLocationAsync(100));
            }
        }

        [TestMethod]
        public async Task Throws_When_ChangeDeliverLocationArrivedAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ChangeDeliverLocationArrivedAsync));

            var parcels = Utils.GetParcels();
            parcels.Add(new Parcel
            {
                Id = 2,
                CustomerId = 2,
                ShipmentId = 3,
                WareHouseId = 1,
                CategoryId = 1,
                Weight = 1,
                DeliverToAddress = true
            });
            var statuses = Utils.GetStatuses();
            var customers = Utils.GetCustomers();
            var shipments = Utils.GetShipments();
            shipments.Add(new Shipment
            {
                Id = 3,
                DepartureDate = System.DateTime.Today.AddDays(5),
                ArrivalDate = System.DateTime.Today.AddDays(10),
                OriginWareHouseId = 1,
                DestinationWareHouseId = 2,
                StatusId = 3
            });
            var categories = Utils.GetCategories();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Categories.AddRangeAsync(categories);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Statuses.AddRangeAsync(statuses);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ParcelService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.ChangeDeliverLocationAsync(2));
            }
        }
    }
}
