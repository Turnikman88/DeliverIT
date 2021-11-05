using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using DeliverIT.Web.Models;
using DeliverIT.Web.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
    public class ShipmentController : Controller
    {
        private readonly IShipmentService _ss;
        private readonly ICheckExistenceService _check;
        public ShipmentController(IShipmentService ss, ICheckExistenceService check)
        {
            this._ss = ss;
            this._check = check;
        }

        public async Task<IActionResult> Index()
        {
            var shipments = await _ss.GetAsync();

            return View(new ShipmentViewModel { Shipments = shipments });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByDestinationWareHouse(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByDestinationWareHouseAsync(int.Parse(model.FilterTag)) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByOriginWareHouseAsync(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByOriginWareHouseAsync(int.Parse(model.FilterTag)) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCustomerId(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByCustomerIdAsync(int.Parse(model.FilterTag)) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCustomerEmail(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByCustomerEmailAsync(model.FilterTag) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCustomerAddress(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByCustomerAddressAsync(model.FilterTag) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByStatusId(ShipmentViewModel model)
        {
            var shipments = new ShipmentViewModel { Shipments = await _ss.FilterByStatusIdAsync(int.Parse(model.FilterTag)) };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ShipmentViewModel();
            model.Statuses = await RenderStatuses();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShipmentViewModel model)
        {
            if (!await _check.WarehouseExists(model.OriginWareHouseId ?? 0))
            {
                this.ModelState.AddModelError("OriginWareHouseId", "Warehouse with this id doen't exists!");
            }
            if (!await _check.WarehouseExists(model.DestinationWareHouseId ?? 0))
            {
                this.ModelState.AddModelError("DestinationWareHouseId", "Warehouse with this id doen't exists!");
            }
            if (model.ArrivalDate < DateTime.Now.Date)
            {
                this.ModelState.AddModelError("ArrivalDate", "Arrival Date can't be before today!");
            }
            if (!this.ModelState.IsValid)
            {
                model.Statuses = await RenderStatuses();

                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Create", model, false) });
            }


            await _ss.PostAsync(model.GetShipmentDTO());

            var shipments = new ShipmentViewModel { Shipments = await _ss.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _ss.GetShipmentByIdAsync(id);

            ShipmentViewModel model = data.GetShipmentViewModel();

            model.Statuses = await RenderStatuses();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ShipmentViewModel model)
        {
            if (!await _check.WarehouseExists(model.OriginWareHouseId ?? 0))
            {
                this.ModelState.AddModelError("OriginWareHouseId", "Warehouse with this id doen't exists!");
            }
            if (!await _check.WarehouseExists(model.DestinationWareHouseId ?? 0))
            {
                this.ModelState.AddModelError("DestinationWareHouseId", "Warehouse with this id doen't exists!");
            }
            if (model.ArrivalDate < DateTime.Now.Date)
            {
                this.ModelState.AddModelError("ArrivalDate", "Arrival Date can't be before today!");
            }
            if (!this.ModelState.IsValid)
            {
                model.Statuses = await RenderStatuses();

                return Json(new { isValid = false, html = await Helper.RenderViewAsync(this, "Update", model, false) });
            }


            await _ss.UpdateAsync(id, model.GetShipmentDTO());

            var shipments = new ShipmentViewModel { Shipments = await _ss.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ss.DeleteAsync(id);

            var shipments = new ShipmentViewModel { Shipments = await _ss.GetAsync() };

            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", shipments, true) });
        }
        private async Task<List<SelectListItem>> RenderStatuses()
        {
            var statuses = await _ss.GetStatusesAsync();

            var model = new List<SelectListItem>();

            foreach (var status in statuses)
            {
                model.Add(new SelectListItem() { Text = status.Name, Value = status.Id.ToString() });
            }

            return model;
        }
    }
}
