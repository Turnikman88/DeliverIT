using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class GetCityByName
    {
        [TestMethod]
        public async Task GetCityByNameShouldReturnCorrectObject()
        {
            var options = Utils.GetOptions(nameof(GetCityByNameShouldReturnCorrectObject));

            var cities = Utils.GetCities();
            var country = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Cities.AddRangeAsync(cities);
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetCityByNameAsync("Sofia");

                Assert.AreEqual(cities[0].Name, result.Name);

                result = await sut.GetCityByNameAsync("Plovdiv");
                Assert.AreEqual(cities[1].Name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task GetCityByShouldThrowExceptionWhenInvalidName()
        {
            var options = Utils.GetOptions(nameof(GetCityByShouldThrowExceptionWhenInvalidName));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetCityByNameAsync("TestNameCity");
            }
        }
    }
}
