using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CustomerServiceTest
{
    [TestClass]
    public class GetCustomer
    {
        [TestMethod]
        public async Task GetAllCustomersAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetAllCustomersAsyncTest));

            var address = Utils.GetAddresses();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var customers = Utils.GetCustomers();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(customers.Count(), result.Count());
            }

        }

        [TestMethod]
        public async Task GetCustomerByName()
        {
            var options = Utils.GetOptions(nameof(GetCustomerByName));

            var address = Utils.GetAddresses();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);

                var pbjectToTest = new Customer()
                {
                    Id = 100,
                    FirstName = "Test",
                    LastName = "TestSUT",
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Customers.AddAsync(pbjectToTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.GetCustomersByNameAsync("Test");

                Assert.AreEqual(1, result.Count());
                Assert.AreEqual("Test", result.First().FirstName);
            }
        }

        [TestMethod]
        public async Task GetCustomerByID()
        {
            var options = Utils.GetOptions(nameof(GetCustomerByID));

            var address = Utils.GetAddresses();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);

                var dto = new Customer()
                {
                    Id = 100,
                    FirstName = "Test",
                    LastName = "TestSUT",
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Customers.AddAsync(dto);
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.GetCustomerByIDAsync(100);

                Assert.AreEqual(100, result.Id);
                Assert.AreEqual("Test", result.FirstName);
                Assert.AreEqual("TestSUT", result.LastName);
                Assert.AreEqual("TestEmail@Test.test", result.Email);
            }
        }


        [TestMethod]
        public async Task GetCustomerByEmail()
        {
            var options = Utils.GetOptions(nameof(GetCustomerByEmail));

            var address = Utils.GetAddresses();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Addresses.AddRangeAsync(address);

                var dto = new Customer()
                {
                    Id = 100,
                    FirstName = "Test",
                    LastName = "TestSUT",
                    Email = "TestEmail@Test.test",
                    Password = "testtest",
                    AddressId = 1
                };
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Customers.AddAsync(dto);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);
                var result = await sut.GetCustomersByEmailAsync("TestEmail");

                Assert.AreEqual(1, result.Count());
                Assert.AreEqual("Test", result.First().FirstName);
                Assert.AreEqual("TestSUT", result.First().LastName);
                Assert.AreEqual("TestEmail@Test.test", result.First().Email);
            }
        }
    }
}
