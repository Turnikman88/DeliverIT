using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using DeliverIT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
    public class CityController : Controller
    {
        private readonly ICityService _cityservice;
        private readonly ICountryService _countryservice;

        public CityController(ICityService cs, ICountryService cos)
        {
            this._cityservice = cs;
            this._countryservice = cos;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await _cityservice.GetAsync();
            var model = new CityViewModel()
            {
                Cities = cities
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByName(CityViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                var empty = new CityViewModel { Cities = await _cityservice.GetAsync() };
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", empty.Cities, true) });
            }

            var city = new CityViewModel { Cities = await _cityservice.GetCitiesByNameAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", city.Cities, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCountryName(CityViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                var empty = new CityViewModel { Cities = await _cityservice.GetAsync() };
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", empty.Cities, true) });
            }

            var city = new CityViewModel { Cities = await _cityservice.GetCitiesByCountryNameAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", city.Cities, true) });
        }

        public async Task<IActionResult> Create()
        {
            var countries = await this._countryservice.GetAsync();

            var model = new CityViewModel();

            foreach (var country in countries)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString()});
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel model)
         {
            var country = await _countryservice.GetCountryByIdAsync(model.CountryId);

            if (await _cityservice.CityExists(model.Name, country.Id))
            {
                this.ModelState.AddModelError("Name", "This city already exists!");
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }

            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }

            await _cityservice.PostAsync(new CityDTO 
            { 
                CountryId = country.Id,
                CountryName = country.Name,
                Name = model.Name
            });

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cityservice.GetAsync(), true) });
        }

        public async Task<IActionResult> Update(string name)
        {
            var countries = await this._countryservice.GetAsync();

            var model = new CityViewModel() { Name = name };

            foreach (var country in countries)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CityViewModel model)
        {
            var country = await _countryservice.GetCountryByIdAsync(model.CountryId);

            if (await _cityservice.CityExists(model.Name, country.Id))
            {
                this.ModelState.AddModelError("Name", "This city already exists!");
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }

            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }


            await _cityservice.UpdateAsync(id, new CityDTO
            {
                CountryId = country.Id,
                CountryName = country.Name,
                Name = model.Name
            });

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cityservice.GetAsync(), true) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityservice.DeleteAsync(id);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Table", await _cityservice.GetAsync(), true) });
        }
    }
}
