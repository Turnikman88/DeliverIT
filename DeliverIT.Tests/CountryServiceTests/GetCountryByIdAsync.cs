using DeliverIT.Models;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CountryServiceTests
{
    [TestClass]
    public class GetCountryByIdAsync
    {
        [TestMethod]
        public async Task GetCountryByIdAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetCountryByIdAsyncTest));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetCountryByIdAsync(1);

                Assert.AreEqual(countries.First().Id, result.Id);
            }
        }

        [TestMethod]
        public async Task Thrwos_When_GetCountryByWrongId()
        {
            var options = Utils.GetOptions(nameof(Thrwos_When_GetCountryByWrongId));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                await Assert.ThrowsExceptionAsync<AppException> (async () => await sut.GetCountryByIdAsync(100));

            }
        }
    }
}
