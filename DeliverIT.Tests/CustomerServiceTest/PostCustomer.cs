using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CustomerServiceTest
{
    [TestClass]
    public class PostCustomer
    {
        [TestMethod]
        public async Task SuccessWhenNewCustomerPostAsync()
        {
            var options = Utils.GetOptions(nameof(SuccessWhenNewCustomerPostAsync));

            var address = Utils.GetAddresses();
            var customers = Utils.GetCustomers();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var parcel = Utils.GetParcels();
            var shipment = Utils.GetShipments();
            var status = Utils.GetStatuses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Parcels.AddRangeAsync(parcel);
                await arrangeContext.Shipments.AddRangeAsync(shipment);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Customers.AddRangeAsync(customers);
                await arrangeContext.SaveChangesAsync();
            }

            var objectToTest = new CustomerDTO()
            {
                Id = 100,
                FirstName = "Test",
                LastName = "TestSUT",
                Email = "TestEmail@Test.test",
                Password = "testtest",
                AddressId = 1
            };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);

                Assert.AreEqual(4, actContext.Customers.Count());

                var result = await sut.PostAsync(objectToTest);

                Assert.IsNotNull(result);
                Assert.AreEqual(5, actContext.Customers.Count());
            }
        }

        [TestMethod]
        public async Task SuccessWhenAnDeltedItemWasSoftRemovedPostAsync()
        {
            var options = Utils.GetOptions(nameof(SuccessWhenAnDeltedItemWasSoftRemovedPostAsync));

            var address = Utils.GetAddresses();
            var customers = Utils.GetCustomers();
            var city = Utils.GetCities();
            var country = Utils.GetCountries();
            var parcel = Utils.GetParcels();
            var shipment = Utils.GetShipments();
            var status = Utils.GetStatuses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Statuses.AddRangeAsync(status);
                await arrangeContext.Parcels.AddRangeAsync(parcel);
                await arrangeContext.Shipments.AddRangeAsync(shipment);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(city);
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.Customers.AddRangeAsync(customers);
                var objectDefaultDeleted = new Customer()
                {
                    Id = 123,
                    FirstName = "NameDeleted",
                    LastName = "Deleted",
                    Email = "TestEmail@Test.test",
                    Password = "deldeldel",
                    AddressId = 1,
                };

                _ = await arrangeContext.Customers.AddAsync(objectDefaultDeleted);
                await arrangeContext.SaveChangesAsync();
            }

            var objectToTest = new CustomerDTO()
            {
                FirstName = "NameDeleted",
                LastName = "Deleted",
                Email = "TestEmail@Test.test",
                Password = "anotherpasword",
                AddressId = 1
            };


            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CustomerService(actContext);

                var itemSet = actContext.Customers.First(x => x.Email == "TestEmail@Test.test");
                itemSet.IsDeleted = true;
                await actContext.SaveChangesAsync();

                var result = await sut.PostAsync(objectToTest);

                Assert.IsNotNull(result);
                Assert.AreEqual(objectToTest.Email, result.Email);
                Assert.AreEqual(123, result.Id);
            }
        }
    }
}
