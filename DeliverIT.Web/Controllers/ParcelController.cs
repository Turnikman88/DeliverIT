using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Attributes;
using DeliverIT.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    public class ParcelController : Controller
    {
        private readonly IParcelService _ps;
        public ParcelController(IParcelService ps)
        {
            this._ps = ps;
        }

        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<IActionResult> CustomerParcels()
        {
            var id = int.Parse(this.HttpContext.Session.GetString(Constants.SESSION_ID_KEY));
            var parcels = await _ps.GetSortedParcelsByCustomerIdAsync(id);
            return View(parcels);
        }

        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<IActionResult> ChangeDeliveryLocation(int id)
        {
            var userId = int.Parse(this.HttpContext.Session.GetString(Constants.SESSION_ID_KEY));

            await _ps.ChangeDeliverLocationAsync(id);

            return Json(new { isValid = true, 
                html = await Helper.RenderViewAsync(this, "_Table", await _ps.GetSortedParcelsByCustomerIdAsync(userId), true) });
        }
    }
}
