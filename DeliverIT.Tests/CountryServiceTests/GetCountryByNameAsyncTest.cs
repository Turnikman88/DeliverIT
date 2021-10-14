using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CountryServiceTests
{
    [TestClass]
    public class GetCountryByNameAsync
    {
        [TestMethod]
        public async Task GetCountryByName()
        {
            var options = Utils.GetOptions(nameof(GetCountryByName));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetCountryByNameAsync("Turkey");

                Assert.AreEqual(countries.Skip(1).First().Id, result.Id);
            }
        }

        [TestMethod]
        public async Task Throws_When_GetCountryByWrongName()
        {
            var options = Utils.GetOptions(nameof(Throws_When_GetCountryByWrongName));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.GetCountryByNameAsync("Qss"));
            }
        }
    }
}
