using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerService _cs;
        private IAddressService _ads;
        private IHostingEnvironment _Environment;
        private IWareHouseService _whs;

        public HomeController(ICustomerService cs, IAddressService ads, IWareHouseService whs, IHostingEnvironment Environment)
        {
            this._cs = cs;
            this._whs = whs;
            this._ads = ads;
            this._Environment = Environment;
        }

        public async Task<IActionResult> Index()
        {
            var indexview = new IndexViewModel();
            indexview.UserCount = await _cs.UserCountAsync();
            var addresses = _whs.GetAddressesObject();
            foreach (var item in addresses)
            {
                indexview.WarehouseLocations.Add(
                    new Maps { 
                        Country = item.City.Country.Name,
                        City = item.City.Name,
                        Address = item.StreetName 
                    });
            }
            
            return View(indexview);
        }

        public async Task<IActionResult> About()
        {
            return View();
        }

        public async Task<IActionResult> Login(UserViewModel user)
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            return View();
        }

        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var imageLink = "https://localhost:5001/images/";

            if (exception != null)
            {
                switch (exception)
                {
                    case AppException e:
                        // custom application error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        imageLink += "400.png";
                        break;
                    case UnauthorizedAppException e:
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        imageLink += "401.png";
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        imageLink += "404.png";
                        break;
                    default:
                        // unhandled error
                        imageLink += "500.png";
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
            else
            {
                imageLink += "404.png";                
            }

            var statuscode = HttpContext.Response.StatusCode;
            return View(new ErrorViewModel { StatusCode = statuscode, Message = exception?.Message ?? "Wrong Address!", ImageLink = imageLink });
        }
    }
}
