using DeliverIT.Models;
using DeliverIT.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Tests.CityServiceTests
{
    [TestClass]
    public class DeleteAsync
    {
        [TestMethod]
        public async Task DeleteShouldSoftDeleteItem()
        {
            var options = Utils.GetOptions(nameof(DeleteShouldSoftDeleteItem));

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
                var result = await sut.DeleteAsync(1);

                Assert.IsFalse(actContext.Cities.Any(x => x.Id == 1));
            }
        }
    }
}
