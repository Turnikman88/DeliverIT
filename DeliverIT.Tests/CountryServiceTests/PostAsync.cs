using DeliverIT.Models;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CountryServiceTests
{
    [TestClass]
    public class PostAsync
    {
        [TestMethod]
        public async Task Success_When_PostCountryAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_PostCountryAsync));

            var dto = new CountryDTO { Name = "Test" };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, actContext.Countries.Count());
            }
        }
        [TestMethod]
        public async Task DeletedCountry_GetsItsId()
        {
            var options = Utils.GetOptions(nameof(DeletedCountry_GetsItsId));

            var dto = new CountryDTO { Name = "Bulgaria" };

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }
            using (var actContext = new DeliverITDBContext(options))
            {
                var deleted = actContext.Countries.FirstOrDefault(x => x.Id == 1);
                deleted.IsDeleted = true;
                await actContext.SaveChangesAsync();

                var sut = new CountryService(actContext);
                var result = await sut.PostAsync(dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Id);
            }
        }

        [TestMethod]
        public async Task Throws_When_PostInvalidCountryName()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostInvalidCountryName));

            var dto = new CountryDTO { Name = null };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.PostAsync(dto));
            }
        }
        [TestMethod]
        public async Task Throws_When_PostExistingCountryName()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostExistingCountryName));

            var countries = Utils.GetCountries();

            var dto = new CountryDTO { Name = "Bulgaria" };

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.PostAsync(dto));
            }
        }
    }
}
