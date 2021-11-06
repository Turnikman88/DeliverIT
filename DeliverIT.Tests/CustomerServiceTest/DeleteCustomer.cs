using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CustomerServiceTest
{
    [TestClass]
    public class DeleteCustomer
    {
        [TestMethod]
        public async Task ShouldRemoveItFromCollection()
        {
            var options = Utils.GetOptions(nameof(ShouldRemoveItFromCollection));

            var address = Utils.GetAddresses();
            var country = Utils.GetCountries();
            var city = Utils.GetCities();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);

                var objectToTest = new Customer()
                {
                    Id = 100,
                    FirstName = "Test",
                    LastName = "TestSUT",
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };

                await arrangeContext.Customers.AddAsync(objectToTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                Assert.AreEqual(1, actContext.Customers.Count());
                var result = await sut.DeleteAsync(100);

                Assert.AreEqual(0, actContext.Customers.Count());
                Assert.AreEqual("Test", result.FirstName);
                Assert.AreEqual(100, result.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task ShouldThrhrowErrorWhenIdNotExisting()
        {
            var options = Utils.GetOptions(nameof(ShouldRemoveItFromCollection));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.DeleteAsync(100);
            }
        }
    }
}
