using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliverIT.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DeliverITDBContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IWareHouseService, WareHouseService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IParcelService, ParcelService>();
            services.AddScoped<IFindUserService, FindUserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICheckExistenceService, CheckExistenceService>();
            services.AddSingleton<IMailSettings, MailSettings>();
            services.AddTransient<IMailService, MailService>();

            return services;
        }
    }
}
