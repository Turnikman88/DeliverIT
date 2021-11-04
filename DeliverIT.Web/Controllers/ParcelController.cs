using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using DeliverIT.Web.Models;
using DeliverIT.Web.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    public class ParcelController : Controller
    {
        private readonly IParcelService _ps;
        private readonly ICheckExistenceService _check;
        public ParcelController(IParcelService ps, ICheckExistenceService check)
        {
            this._ps = ps;
            this._check = check;
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Index()
        {
            var parcels = await _ps.GetAsync();

            return View(new ParcelViewModel { Parcels = parcels});
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> GetParcelsByCustomerId(ParcelViewModel model)
        {
            var parcels = new ParcelViewModel { Parcels = await _ps.GetSortedParcelsByCustomerIdAsync(int.Parse(model.FilterTag)) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> SortByWeight()
        {
            var parcels = new ParcelViewModel { Parcels = await _ps.SortByWeightAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        } 
        
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> SortByArrivalDate()
        {
            var parcels = new ParcelViewModel { Parcels = await _ps.SortByArrivalDateAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> SortByWeightAndArrivalDate()
        {
            var parcels = new ParcelViewModel { Parcels = await _ps.SortByWeightAndArrivalDateAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ParcelViewModel model = await RenderCategories();

            return View(model);
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> Create(ParcelViewModel model)
        {
            if (!await _check.CustomerExists(model.CustomerId ?? 0))
            {
                this.ModelState.AddModelError("CustomerId", "Customer with this id doen't exists!");
            } 
            if (!await _check.ShipmentExists(model.ShipmentId ?? 0))
            {
                this.ModelState.AddModelError("ShipmentId", "Shipment with this id doen't exists!");
            }
            if (!await _check.WarehouseExists(model.WareHouseId))
            {
                this.ModelState.AddModelError("WarehouseId", "Warehouse with this id doen't exists!");
            }
            if (!this.ModelState.IsValid)
            {
                ParcelViewModel errorModel = await RenderCategories();

                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", errorModel, false) });
            }


            await _ps.PostAsync(model.GetParcelDTO());

            var parcels = new ParcelViewModel { Parcels = await _ps.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _ps.GetParcelByIdAsync(id);
            
            ParcelViewModel model = data.GetParcelViewModel();

            var categories = await _ps.GetCategoriesAsync();

            foreach (var category in categories)
            {
                model.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }

            return View(model);
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ps.DeleteAsync(id);

            var parcels = new ParcelViewModel { Parcels = await _ps.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", parcels, true) });
        }

        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<IActionResult> CustomerParcels()
        {
            var id = int.Parse(this.HttpContext.Session.GetString(Constants.SESSION_ID_KEY));
            var parcels = new ParcelViewModel { Parcels = await _ps.GetSortedParcelsByCustomerIdAsync(id) };
            return View(parcels);
        }

        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<IActionResult> ChangeDeliveryLocation(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString(Constants.SESSION_ID_KEY));

            await _ps.ChangeDeliverLocationAsync(id);

            var parcels = new ParcelViewModel { Parcels = await _ps.GetSortedParcelsByCustomerIdAsync(userId) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_CustomerTable", parcels, true) });
        }
        private async Task<ParcelViewModel> RenderCategories()
        {
            var categories = await _ps.GetCategoriesAsync();

            var model = new ParcelViewModel();

            foreach (var category in categories)
            {
                model.Categories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }

            return model;
        }
    }
}
