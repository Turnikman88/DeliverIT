using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CustomerServiceTest
{
    [TestClass]
    public class UpdateCustomer
    {
        [TestMethod]
        public async Task ShouldUpdateCustomerDetails()
        {
            var options = Utils.GetOptions(nameof(ShouldUpdateCustomerDetails));

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
                var objectToTest = new CustomerDTO()
                {
                    Id = 100,
                    FirstName = "Test",
                    LastName = "TestSUT",
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };

                var sut = new CustomerService(actContext);
                var result = await sut.UpdateAsync(1, objectToTest);

                Assert.AreEqual(objectToTest.FirstName, result.FirstName);
                Assert.AreEqual(objectToTest.LastName, result.LastName);
                Assert.AreEqual(objectToTest.Email, result.Email);
                Assert.AreEqual(1, result.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task ShouldThrowExceptionWhenNullDetails()
        {
            var options = Utils.GetOptions(nameof(ShouldThrowExceptionWhenNullDetails));

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
                var objectToTest = new CustomerDTO()
                {
                    Id = 100,
                    FirstName = null,
                    LastName = null,
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };

                var sut = new CustomerService(actContext);
                var result = await sut.UpdateAsync(1, objectToTest);
            }
        }
    }
}
