using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.ShipmentServiceTest
{
    [TestClass]
    public class FilterByCustomerAddressAsync
    {
        [TestMethod]
        public async Task Success_When_FilterByCustomerAddressAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_FilterByCustomerAddressAsync));

            var shipments = Utils.GetShipments();

            var status = Utils.GetStatuses();
            var parcels = Utils.GetParcels();
            var addresses = Utils.GetAddresses();
            var customers = Utils.GetCustomers();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(addresses);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Shipments.AddRangeAsync(shipments);
                await arrangeContext.Parcels.AddRangeAsync(parcels);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = await sut.FilterByCustomerAddressAsync("Qss");

                Assert.AreEqual(0, result.Count());

                result = await sut.FilterByCustomerAddressAsync("as");

                Assert.AreEqual(1, result.Count());
            }
        }
    }
}
