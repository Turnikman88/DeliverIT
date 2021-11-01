using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Models;
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
            var countries = await _cs.GetAsync();
            return View(countries);
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Create(CountryDTO model)
        {
            await _cs.PostAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _cs.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
