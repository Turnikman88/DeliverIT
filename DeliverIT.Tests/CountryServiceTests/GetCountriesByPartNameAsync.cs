using DeliverIT.Models;
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
    public class GetCountriesByPartNameAsync
    {
        [TestMethod]
        public async Task GetCountriesByPartNameAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetCountriesByPartNameAsyncTest));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetCountriesByPartNameAsync("u");

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task Empty_When_GetCountriesByPartNameAsync()
        {
            var options = Utils.GetOptions(nameof(Empty_When_GetCountriesByPartNameAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetCountriesByPartNameAsync("Qss");

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
