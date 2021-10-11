using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class UpdateAsync
    {
        [TestMethod]
        public async Task UpdateAsyncShouldUpdateCorrectInformation()
        {
            var options = Utils.GetOptions(nameof(UpdateAsyncShouldUpdateCorrectInformation));

            var country = Utils.GetCountries();
            var cities = Utils.GetCities();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(cities);
                await arrangeContext.SaveChangesAsync();
            }

            var dto = new CityDTO
            {
                Id = 1,
                Name = "test city",
                CountryId = 1,
                CountryName = "Bulgaria"
            };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.UpdateAsync(1, dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(dto.Id, result.Id);
                Assert.AreEqual(dto.Name, result.Name);

                dto.Name = "Another test";
                result = await sut.UpdateAsync(1, dto);

                Assert.AreEqual(dto.Name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task UpdateAsyncShouldThrowExceptionWhenNameIsNull()
        {
            var options = Utils.GetOptions(nameof(UpdateAsyncShouldThrowExceptionWhenNameIsNull));

            var country = Utils.GetCountries();
            var cities = Utils.GetCities();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(cities);
                await arrangeContext.SaveChangesAsync();
            }

            var dto = new CityDTO
            {
                Id = 1,
                Name = null,
                CountryId = 1,
                CountryName = "Bulgaria"
            };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.UpdateAsync(1, dto);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task UpdateAsyncShouldThrowExceptionWhenIdIsInvalid()
        {
            var options = Utils.GetOptions(nameof(UpdateAsyncShouldThrowExceptionWhenIdIsInvalid));

            var country = Utils.GetCountries();
            var cities = Utils.GetCities();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.Cities.AddRangeAsync(cities);
                await arrangeContext.SaveChangesAsync();
            }

            var dto = new CityDTO();

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.UpdateAsync(123, dto);
            }
        }
    }
}
