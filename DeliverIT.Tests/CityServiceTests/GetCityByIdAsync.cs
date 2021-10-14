using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class GetCutyByIdAsync
    {
        [TestMethod]
        public async Task GetCityByIDShouldReturnCorrectObject()
        {
            var options = Utils.GetOptions(nameof(GetCityByIDShouldReturnCorrectObject));

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
                var result = await sut.GetCityByIdAsync(2);

                Assert.AreEqual(cities[1].Name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task GetCityByIdShouldThrowExceptionWhenIdIsNegative()
        {
            var options = Utils.GetOptions(nameof(GetCityByIdShouldThrowExceptionWhenIdIsNegative));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetCityByIdAsync(-12);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task GetCityByIdShouldThrowExceptionWhenIdIsOutOfRange()
        {
            var options = Utils.GetOptions(nameof(GetCityByIdShouldThrowExceptionWhenIdIsOutOfRange));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.GetCityByIdAsync(12);
            }
        }
    }

}
