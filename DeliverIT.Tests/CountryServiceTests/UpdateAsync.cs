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
    public class UpdateAsync
    {
        [TestMethod]
        public async Task Success_When_UpdateAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_UpdateAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }
            var dto = new CountryDTO { Name = "Test" };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);
                var result = await sut.UpdateAsync(1, dto);

                Assert.IsNotNull(result);
                Assert.AreEqual(actContext.Countries.FirstOrDefault().Name, "Test");
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateNullAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateNullAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }
            var dto = new CountryDTO { Name = null };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(1, dto));
            }
        }

        [TestMethod]
        public async Task Throws_When_UpdateExistingAsync()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateExistingAsync));

            var countries = Utils.GetCountries();

            using (var arrangeContext = new DeliverITDBContext(options))
            {
                await arrangeContext.Countries.AddRangeAsync(countries);
                await arrangeContext.SaveChangesAsync();
            }
            var dto = new CountryDTO { Name = "Bulgaria" };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.UpdateAsync(1, dto));
            }
        }
    }
}
