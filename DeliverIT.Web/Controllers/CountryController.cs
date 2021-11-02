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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
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

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
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
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _cs.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
