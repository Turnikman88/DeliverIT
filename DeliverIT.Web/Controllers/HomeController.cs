using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
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
        private IWareHouseService _whs;
        private IMailService _ms;

        public HomeController(ICustomerService cs, IWareHouseService whs, IMailService ms)
        {
            this._cs = cs;
            this._whs = whs;
            this._ms = ms;
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

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {            
            return View(new MailDTO());
        }
        [HttpPost]
        public async Task<IActionResult> Contact(MailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            await _ms.SendEmailAsync(model);

            model.isSent = true;
            return this.View();
        }
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var imageLink = $"{Constants.DOMAIN_NAME}/images/";

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
