using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CustomerServiceTest
{
    [TestClass]
    public class UserCount
    {
        [TestMethod]
        public async Task CustomerCountShouldReturnExactCustomers()
        {
            var options = Utils.GetOptions(nameof(CustomerCountShouldReturnExactCustomers));

            var address = Utils.GetAddresses();
            var customers = Utils.GetCustomers();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.UserCountAsync();

                Assert.AreEqual(customers.Count, result);
            }

        }
    }
}
