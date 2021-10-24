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

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            model.AddressId = await GetAddressID(model);
            var toCustomer = model.GetDTO();
            await this._cs.PostAsync(toCustomer);
            return this.Redirect(nameof(Login));
        }

        private async Task<int> GetAddressID(UserViewModel model)
        {
            return await _ads.AddressToID(model.Address, model.City, model.Country);
        }

        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error?.Message;
            var statuscode = HttpContext.Response.StatusCode;
            return View(new ErrorViewModel {StatusCode = statuscode, Message = exception, ImageLink = $"https://http.cat/{statuscode}" });
        }
    }
}
