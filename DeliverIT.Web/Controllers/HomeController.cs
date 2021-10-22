using DeliverIT.Services.Contracts;
using DeliverIT.Web.Models;
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

        public HomeController(ICustomerService cs)
        {
            this._cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }

        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            return View();
        }

        public IActionResult Register()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            return View(model);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
