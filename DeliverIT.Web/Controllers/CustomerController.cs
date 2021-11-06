using DeliverIT.Services.Contracts;
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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _cs;
        public CustomerController(ICustomerService cs)
        {
            this._cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _cs.GetAsync();
            var model = new CustomerViewModel()
            {
                Customers = customers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByName(CustomerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
            }

            var customers = new CustomerViewModel { Customers = await _cs.GetCustomersByNameAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", customers.Customers, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByEmail(CustomerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
            }

            var customers = new CustomerViewModel { Customers = await _cs.GetCustomersByEmailAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", customers.Customers, true) });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCountry(CustomerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FilterTag))
            {
                return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
            }
            var customers = new CustomerViewModel { Customers = await _cs.GetCustomersByCountryAsync(model.FilterTag) };
            return Json(new { isValid = true, html = await Helper.RenderViewAsync(this, "_Table", customers.Customers, true) });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _cs.DeleteAsync(id);

            return Json(new { html = await Helper.RenderViewAsync(this, "_Table", await _cs.GetAsync(), true) });
        }
    }
}
