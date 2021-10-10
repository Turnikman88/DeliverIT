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
    public class GetAllAsync
    {
        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetAllAsyncTest));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(countries.Count(), result.Count());
            }
        }
        [TestMethod]
        public async Task Empty_When_GetAllAsyncTest()
        {
            var options = Utils.GetOptions(nameof(GetAllAsyncTest));

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.GetAsync();

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
