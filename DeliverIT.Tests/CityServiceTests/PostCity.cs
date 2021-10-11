using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class PostCity
    {
        [TestMethod]
        public async Task SuccessfulCreatingCity()
        {
            var options = Utils.GetOptions(nameof(SuccessfulCreatingCity));

            var country = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(country);
                await arrangeContext.SaveChangesAsync();
            }

            var dto = new CityDTO
            {
                Id = 100,
                Name = "test city",
                CountryId = 1,
                CountryName = "Bulgaria"
            };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, actContext.Cities.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AppException))]
        public async Task Throws_When_PostInvalidName()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostInvalidName));

            var dto = new CityDTO { Name = null };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CityService(actContext);
                var result = await sut.PostAsync(dto);
            }
        }

        [TestMethod]
        public async Task DeletedCityGetsItsOldIdWhenPostedAgain()
        {
            var options = Utils.GetOptions(nameof(DeletedCityGetsItsOldIdWhenPostedAgain));

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
                //var deletedObj = sut.DeleteAsync(1);
                var deletedObj = actContext.Cities.FindAsync(1);
                deletedObj.Result.IsDeleted = true;
                await actContext.SaveChangesAsync();


                Assert.AreEqual(cities[0].Id, deletedObj.Result.Id);
                // Assert.AreEqual(cities[0].Name, deletedObj.Name); //Sofia
                // Assert.AreEqual(cities[0].CountryId, deletedObj.CountryId); //Bulgaria                

                var dto = new CityDTO
                {
                    Id = 123,
                    Name = "Sofia",
                    CountryId = 1,
                    CountryName = "Bulgaria"
                };

                var result = await sut.PostAsync(dto);
                Assert.AreEqual(cities[0].Id, result.Id);
                Assert.AreEqual(cities[0].Name, result.Name);
            }
        }
    }
}
