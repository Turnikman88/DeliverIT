using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliverIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _ps;

        public ParcelController(IParcelService ps)
        {
            this._ps = ps;
        }

        [HttpGet("incoming")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<ActionResult<ParcelDTO>> ListCustomerIncomingParcelsAsync()
        {
            var role = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (role == Constants.ROLE_USER)
            {
                return this.Ok(await _ps.ListCustomerIncomingParcelsAsync(int.Parse(userId))); 
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> GetParcelByIdAsync(int id)
        {
            return this.Ok(await _ps.GetParcelByIdAsync(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> GetParcelsAsync()
        {           
            return this.Ok(await _ps.GetAsync());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> CreateParcelAsync(ParcelDTO obj)
        {
            return this.Ok(await _ps.PostAsync(obj));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> UpdateParcelAsync(int id, ParcelDTO obj)
        {
            return this.Ok(await _ps.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<ParcelDTO>> DeleteParcelAsync(int id)
        {
            return this.Ok(await _ps.DeleteAsync(id));
        }

        [HttpGet("filter/customer/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> FilterByCustomerIdAsync(int customerId)
        {
            var role = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (role == Constants.ROLE_EMPLOYEE)
            { 
                return this.Ok(await _ps.FilterByCustomerIdAsync(customerId));                
            }
            else if (role == Constants.ROLE_USER)
            {
                if (customerId == int.Parse(userId))
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<ActionResult<IEnumerable<string>>> GetShipmentStatusAsync(int customerId)
        {
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (customerId == int.Parse(userId))
            {
                return this.Ok(await _ps.GetShipmentStatusAsync(customerId));
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpPut("deliveraddress/{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_USER)]
        public async Task<ActionResult<IEnumerable<string
            >>> ChangeDeliverLocationAsync(int customerId)
        {
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (customerId == int.Parse(userId))
            {
                return this.Ok(await _ps.ChangeDeliverLocationAsync(customerId));
            }

            return this.Unauthorized(Constants.NOT_LOGGED);
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> MultiFilterAsync(int? id, int? customerId,
            int? shipmentId, int? warehouseId, int? categoryId, string categoryName, double? minWeight, double? maxWeight)
        {            
            return this.Ok(await _ps.MultiFilterAsync(id, customerId, shipmentId, warehouseId, categoryId, categoryName, minWeight, maxWeight));
        }

        [HttpGet("sort/weight")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAsync()
        {
            return this.Ok(await _ps.SortByWeightAsync());
        }

        [HttpGet("sort/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByArrivalDateAsync()
        {
            return this.Ok(await _ps.SortByArrivalDateAsync());
        }

        [HttpGet("sort/weight/arrival")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = Constants.ROLE_EMPLOYEE)]
        public async Task<ActionResult<IEnumerable<ParcelDTO>>> SortByWeightAndArrivalDateAsync()
        {
            return this.Ok(await _ps.SortByWeightAndArrivalDateAsync());
        }
    }
}
