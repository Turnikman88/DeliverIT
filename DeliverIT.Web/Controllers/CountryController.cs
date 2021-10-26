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
    public class CountryController : Controller
    {
        private readonly ICountryService _cs;
        public CountryController(ICountryService cs)
        {
            this._cs = cs;
        }


        public async Task<IActionResult> Index()
        {
            if (!this.HttpContext.Session.Keys.Contains("CurrentUser"))
            {
                return this.RedirectToAction("Login", "Auth");
            }
            var credentials = HttpContext.Items["CurrentUser"].ToString();

            return View();
        }


    }
}
