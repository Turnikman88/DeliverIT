using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DeliverITDBContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IWareHouseService, WareHouseService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IParcelService, ParcelService>();
            services.AddScoped<IAppAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
