using DeliverIT.Services.Contracts;
using DeliverIT.Web.Models;
using DeliverIT.Web.Models.Mappers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerService _cs;
        private IAddressService _ads;

        public HomeController(ICustomerService cs, IAddressService ads)
        {
            this._cs = cs;
            this._ads = ads;
        }

        public async Task<IActionResult> Index()
        {
            return View();
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
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error?.Message;
            var statuscode = HttpContext.Response.StatusCode;
            return View(new ErrorViewModel {StatusCode = statuscode, Message = exception, ImageLink = $"https://http.cat/{statuscode}" });
        }
    }
}
