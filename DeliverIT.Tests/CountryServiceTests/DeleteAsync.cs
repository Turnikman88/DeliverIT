using DeliverIT.Models;
using DeliverIT.Services.DTOs;
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
    public class DeleteAsync
    {
        [TestMethod]
        public async Task Success_When_DeleteAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_DeleteAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }
           
            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.DeleteAsync(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(countries.Count() - 1, actContext.Countries.Count());
            }
        }

        [TestMethod]
        public async Task Throws_When_DeleteAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_DeleteAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.DeleteAsync(100));
            }
        }
    }
}
