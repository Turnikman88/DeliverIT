using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using DeliverIT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    [Authorize(Roles = Constants.ROLE_EMPLOYEE)]

    public class CountryController : Controller
    {
        private readonly ICountryService _cs;
        public CountryController(ICountryService cs)
        {
            this._cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _cs.GetAsync();
            return View(new CountryViewModel { Countries = countries });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByName(CountryViewModel model)
        {
            var countries = new CountryViewModel { Countries = await _cs.GetCountriesByPartNameAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", countries, true) });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryViewModel model)
        {
            if (await _cs.CountryExists(model.Name))
            {
                this.ModelState.AddModelError("Name", "Country with this name already exists!");
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }

            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }


            await _cs.PostAsync(new CountryDTO { Name = model.Name});
            
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) }); 
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CountryViewModel model)
        {
            if (await _cs.CountryExists(model.Name))
            {
                this.ModelState.AddModelError("Name", "Country with this name already exists!");
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }

            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }


            await _cs.UpdateAsync(id, new CountryDTO { Name = model.Name });

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _cs.DeleteAsync(id);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
        }
    }
}
