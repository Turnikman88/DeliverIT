using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _ps;

        public ParcelController(IParcelService ps, ICustomerService cs, IAuthenticationService auth)
        {
            this._ps = ps;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync(int id)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync(ParcelDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync(int id, ParcelDTO obj)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync(int id)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.DeleteAsync(id));
        }

        [HttpGet("filter/customer/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync(int customerId)
        {            
            if (this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            { 
                return this.Ok(await _ps.FilterByCustomerIdAsync(customerId));                
            }

            if (Request.Cookies[Constants.KEY_USER_ID] != null)
            {
                if (customerId == int.Parse(Request.Cookies[Constants.KEY_USER_ID]))
                {
                    return this.Ok(await _ps.FilterByCustomerIdAsync(customerId));
                }

                return this.Unauthorized(Constants.WRONG_ID);
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpGet("filter/statuses/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<string>>> GetShipmentStatusAsync(int customerId)
        {
            if (Request.Cookies[Constants.KEY_USER_ID] != null && customerId == int.Parse(Request.Cookies[Constants.KEY_USER_ID]))
            {
                return this.Ok(await _ps.GetShipmentStatusAsync(customerId));
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpPut("deliveraddress/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<string
            >>> ChangeDeliverLocationAsync(int customerId)
        {
            if (Request.Cookies[Constants.KEY_USER_ID] != null && customerId == int.Parse(Request.Cookies[Constants.KEY_USER_ID]))
            {
                return this.Ok(await _ps.ChangeDeliverLocationAsync(customerId));
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync(int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync()
        {
            if (!this.Request.Cookies.ContainsKey(Constants.KEY_EMPLOYEE_ID))
            {
                return this.Unauthorized(Constants.NOT_EMPLOYEE);
            }

            return this.Ok(await _ps.SortByWeightAndArrivalDateAsync());
        }
    }
}
