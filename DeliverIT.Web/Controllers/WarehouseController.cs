using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using DeliverIT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
    public class WarehouseController : Controller
    {
        private readonly IWareHouseService _ws;
        private readonly ICountryService _cs;
        private readonly ICityService _cits;
        private readonly IAddressService _ads;
        public WarehouseController(IWareHouseService ws, ICityService cits, ICountryService cs, IAddressService ads)
        {
            this._ws = ws;
            this._cits = cits;
            this._cs = cs;
            this._ads = ads;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await this._ws.GetAsync();
            var model = new WarehouseViewModel()
            {
                Warehouses = warehouses
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var countries = await RenderCountries();

            var model = new WarehouseViewModel()
            {
                Countries = countries
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCountryName(WarehouseViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                var empty = new WarehouseViewModel { Warehouses = await _ws.GetAsync() };
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", empty.Warehouses, true) });
            }

            var filtered = new WarehouseViewModel { Warehouses = await _ws.GetWareHouseByCountryAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", filtered.Warehouses, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCityName(CityViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                var empty = new WarehouseViewModel { Warehouses = await _ws.GetAsync() };
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", empty.Warehouses, true) });
            }

            var filtered = new WarehouseViewModel { Warehouses = await _ws.GetWareHouseByCityAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", filtered.Warehouses, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Create(WarehouseViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }

            var adressId = await this._ads.AddressToID(model.Address, model.City, model.Country);

            await _ws.PostAsync(new WareHouseDTO
            {
                AddressId = adressId,
                City = model.City,
                Country = model.Country,
                StreetName = model.Address,
            });

            var warehouses = new WarehouseViewModel { Warehouses = await _ws.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", warehouses.Warehouses, true) });
        }

        public async Task<IActionResult> Update(int id)
        {
            var currentWarehouse = await _ws.GetWareHouseByIdAsync(id);
            var countries = await RenderCountries();

            var model = new WarehouseViewModel
            {
                City = currentWarehouse.City,
                Address = currentWarehouse.StreetName,
                Country = currentWarehouse.Country,
                Countries = countries
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WarehouseViewModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }

            var adressId = await this._ads.AddressToID(model.Address, model.City, model.Country);

            await _ws.UpdateAsync(id, new WareHouseDTO
            {
                AddressId = adressId,
                City = model.City,
                Country = model.Country,
                StreetName = model.Address,
            });

            var warehouses = new WarehouseViewModel { Warehouses = await _ws.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", warehouses.Warehouses, true) });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ws.DeleteAsync(id);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Table", await _ws.GetAsync(), true) });
        }

        [Route("[controller]/Cities/{countryName}")]
        public async Task<IActionResult> Cities(string countryName)
        {
            var getCities = new JsonResult(await _ads.GetCities(countryName));
            return getCities;
        }

        private async Task<List<SelectListItem>> RenderCountries()
        {
            var countries = await _ads.GetCountries();

            var model = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.Add(new SelectListItem() { Text = country.Name, Value = country.Name });
            }

            return model;
        }
    }
}
