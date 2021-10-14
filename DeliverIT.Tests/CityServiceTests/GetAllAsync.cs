using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class GetAllAsync
    {
        [TestMethod]
        public async Task GetAllCitiesAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetAllCitiesAsyncTest));

            var cities = Utils.GetCities();
            var country = Utils.GetCountries();
            var address = Utils.GetAddresses();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Cities.AddRangeAsync(cities);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Addresses.AddRangeAsync(address);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(cities.Count(), result.Count());
            }
        }

        [TestMethod]
        public async Task Empty_When_GetAllCitiesAsyncTest()
        {
            var options = Utils.GetOptions(nameof(Empty_When_GetAllCitiesAsyncTest));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
