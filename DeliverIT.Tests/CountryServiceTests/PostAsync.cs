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
    public class PostAsync
    {
        [TestMethod]
        public async Task Success_When_PostAsync()
        {
            var options = Utils.GetOptions(nameof(Success_When_PostAsync));

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
        public async Task Throws_When_PostInvalidName()
        {
            var options = Utils.GetOptions(nameof(Throws_When_PostInvalidName));

            var dto = new CountryDTO { Name = null };

            using (var actContext = new DeliverITDBContext(options))
            {
                var sut = new CountryService(actContext);

                await Assert.ThrowsExceptionAsync<AppException>(async () => await sut.PostAsync(dto));
            }
        }
    }
}
