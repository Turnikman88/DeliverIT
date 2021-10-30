﻿using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Index()
        {           
            return View();
        }
    }
}
